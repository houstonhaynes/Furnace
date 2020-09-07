﻿namespace DiffSharp.Util

open System.Collections.Generic
open System.Diagnostics.CodeAnalysis
open System.IO
open System.IO.Compression
open System.Net
open System.Runtime.Serialization
open System.Runtime.Serialization.Formatters.Binary

/// <summary>
///   Contains extensions to the F# Array module. 
/// </summary>
///
/// <namespacedoc>
///   <summary>Contains utilities and library extensions related to the DiffSharp programming model.</summary>
/// </namespacedoc>
module Array =

    /// Determine if all values of the first array lie within the given tolerances of the second array
    [<ExcludeFromCodeCoverage>]
    let inline allClose (relativeTolerance:'T) (absoluteTolerance:'T) (array1:'T[]) (array2:'T[]) =
        let dim1 = array1.Length
        let dim2 = array2.Length
        if dim1 <> dim2 then false
        else (array1,array2) ||> Array.forall2 (fun a b -> abs(a-b) <= absoluteTolerance + relativeTolerance*abs(b)) 

    /// Get the cumulative sum of the input array
    [<ExcludeFromCodeCoverage>]
    let inline cumulativeSum (a:_[]) = (Array.scan (+) LanguagePrimitives.GenericZero a).[1..]

    /// Get the unique counts of the input array
    let getUniqueCounts (sorted:bool) (values:'T[]) =
        let counts = Dictionary<'T, int>()
        for v in values do
            if counts.ContainsKey(v) then counts.[v] <- counts.[v] + 1 else counts.[v] <- 1
        if sorted then
            counts |> Array.ofSeq |> Array.sortByDescending (fun (KeyValue(_, v)) -> v) |> Array.map (fun (KeyValue(k, v)) -> k, v) |> Array.unzip
        else
            counts |> Array.ofSeq |> Array.map (fun (KeyValue(k, v)) -> k, v) |> Array.unzip

/// Contains extensions to the F# Seq module. 
module Seq =

    /// Get the index of the maximum element of the sequence
    let maxIndex seq =  seq |> Seq.mapi (fun i x -> i, x) |> Seq.maxBy snd |> fst

    /// Get the index of the minimum element of the sequence
    let minIndex seq =  seq |> Seq.mapi (fun i x -> i, x) |> Seq.minBy snd |> fst

    /// Indicates if all elements of the sequence are equal
    let allEqual (items:seq<'T>) =
        let item0 = items |> Seq.head
        items |> Seq.forall ((=) item0)

    /// Get the duplicate elements in the sequence
    let duplicates l =
       l |> List.ofSeq
       |> List.groupBy id
       |> List.choose ( function
              | _, x::_::_ -> Some x
              | _ -> None )

    /// Indicates if a sequence has duplicate elements
    let hasDuplicates l =
        duplicates l |> List.isEmpty |> not

/// Contains extensions related to .NET dictionaries. 
module Dictionary =

    /// Get a fresh array containing the keys of the dictionary
    let copyKeys (dictionary:Dictionary<'Key, 'Value>) =
        let keys = Array.zeroCreate dictionary.Count
        dictionary.Keys.CopyTo(keys, 0)
        keys

    /// Get a fresh array containing the values of the dictionary
    let copyValues (dictionary:Dictionary<'Key, 'Value>) =
        let values = Array.zeroCreate dictionary.Count
        dictionary.Values.CopyTo(values, 0)
        values

/// Contains auto-opened extensions to the F# programming model
[<AutoOpen>]
module ExtensionAutoOpens =

    /// Indicates if a value is not null
    [<ExcludeFromCodeCoverage>]
    let inline notNull value = not (obj.ReferenceEquals(value, null))

    /// Return a function that memoizes the given function using a lookaside table
    let memoize fn =
        let cache = new Dictionary<_,_>()
        fun x ->
            match cache.TryGetValue x with
            | true, v -> v
            | false, _ ->
                let v = fn x
                cache.Add(x,v)
                v

    /// Synchronously download the given URL to the given local file
    let download (url:string) (localFileName:string) =
        let wc = new WebClient()
        printfn "Downloading %A to %A" url localFileName
        wc.DownloadFile(url, localFileName)
        wc.Dispose()

    /// Save the given value to the given local file using binary serialization
    let saveBinary (object: 'T) (fileName:string) =
        let formatter = BinaryFormatter()
        let fs = new FileStream(fileName, FileMode.Create)
        let cs = new GZipStream(fs, CompressionMode.Compress)
        try
            formatter.Serialize(cs, object)
            cs.Flush()
            cs.Close()
            fs.Close()
        with
        | :? SerializationException as e -> failwithf "Cannot save to file. %A" e.Message

    /// Load the given value from the given local file using binary serialization
    let loadBinary (fileName:string):'T =
        let formatter = BinaryFormatter()
        let fs = new FileStream(fileName, FileMode.Open)
        let cs = new GZipStream(fs, CompressionMode.Decompress)
        try
            let object = formatter.Deserialize(cs) :?> 'T
            cs.Close()
            fs.Close()
            object
        with
        | :? SerializationException as e -> failwithf "Cannot load from file. %A" e.Message

