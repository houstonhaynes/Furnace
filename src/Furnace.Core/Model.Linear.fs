// Copyright (c) 2016-     University of Oxford (Atılım Güneş Baydin <gunes@robots.ox.ac.uk>)
// and other contributors, see LICENSE in root of repository.
//
// BSD 2-Clause License. See LICENSE in root of repository.

namespace Furnace.Model

open Furnace

/// <summary>A model that applies a linear transformation to the incoming data: \(y = xA^T + b\)</summary>
type Linear(inFeatures, outFeatures, ?bias:bool) =
    inherit Model()
    let biasv = defaultArg bias true
    let w = Parameter(Weight.kaiming(inFeatures, outFeatures))
    let k = 1./sqrt (float outFeatures)
    let b = Parameter(if biasv then Weight.uniform([|outFeatures|], k) else FurnaceImage.tensor([]))
    do base.addParameter((w, "Linear-weight"), (b, "Linear-bias"))

    /// <summary>Get or set the weight parameter of the model</summary>
    member _.weight
        with get() = w.value
        and set v = w.value <- v

    /// <summary>Get or set the bias parameter of the model</summary>
    member _.bias
        with get() = b.value
        and set v = b.value <- v

    /// <summary>TBD</summary>
    override _.ToString() = sprintf "Linear(%A, %A)" inFeatures outFeatures

    /// <summary>TBD</summary>
    override _.forward(value) =
        let f = FurnaceImage.matmul(value, w.value)
        if biasv then f + b.value else f