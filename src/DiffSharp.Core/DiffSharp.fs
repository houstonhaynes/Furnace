namespace DiffSharp

open DiffSharp.Backends
open DiffSharp.Util

/// Tensor operations

type dsharp =
    static member tensor(value:obj, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor.create(value=value, ?dtype=dtype, ?device=device, ?backend=backend)
    static member seed(?seed:int) = BackendStatics.Seed(?seed=seed)
    static member isTensor(value:obj) = value :? Tensor
    static member save(tensor:Tensor, fileName) = tensor.save(fileName)
    static member load(fileName) = Tensor.load(fileName)
    static member zero(?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Zero(?dtype=dtype, ?device=device, ?backend=backend))
    static member zeros(shape:seq<int>, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Zeros(shape|>Seq.toArray, ?dtype=dtype, ?device=device, ?backend=backend))
    static member zeros(length:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Zeros([|length|], ?dtype=dtype, ?device=device, ?backend=backend))
    static member one(?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.One(?dtype=dtype, ?device=device, ?backend=backend))
    static member ones(shape:seq<int>, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Ones(shape|>Seq.toArray, ?dtype=dtype, ?device=device, ?backend=backend))
    static member ones(length:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Ones([|length|], ?dtype=dtype, ?device=device, ?backend=backend))
    static member full(shape:seq<int>, value:obj, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Full(shape|>Seq.toArray, value, ?dtype=dtype, ?device=device, ?backend=backend))
    static member full(length:int, value:scalar, ?dtype:Dtype, ?device:Device, ?backend:Backend) = dsharp.zero(?dtype=dtype, ?device=device, ?backend=backend).fullLike(value, [|length|])

    /// <summary>
    /// Returns a 1-D tensor of size \(\left\lceil \frac{\text{end} - \text{start}}{\text{step}} \right\rceil\)
    /// with values from the interval [start, end) taken with common difference step beginning from start.
    /// </summary>
    /// 
    /// <remarks>
    ///  Non-integer steps may be subject to floating point rounding errors when comparing against end.
    /// </remarks>
    static member arange(endVal:float, ?startVal:float, ?step:float, ?dtype:Dtype, ?device:Device, ?backend:Backend) = dsharp.zero(?dtype=dtype, ?device=device, ?backend=backend).arangeLike(endVal=endVal, ?startVal=startVal, ?step=step)

    static member arange(endVal:int, ?startVal:int, ?step:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = dsharp.zero(?dtype=dtype, ?device=device, ?backend=backend).arangeLike(endVal=endVal, ?startVal=startVal, ?step=step)
    static member onehot(length:int, hot:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = dsharp.zero(?dtype=dtype, ?device=device, ?backend=backend).onehotLike(length, hot)
    static member rand(shape:seq<int>, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Random(shape|>Seq.toArray, ?dtype=dtype, ?device=device, ?backend=backend))
    static member rand(length:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.Random([|length|], ?dtype=dtype, ?device=device, ?backend=backend))
    static member randn(shape:seq<int>, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.RandomNormal(shape|>Seq.toArray, ?dtype=dtype, ?device=device, ?backend=backend))
    static member randn(length:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.RandomNormal([|length|], ?dtype=dtype, ?device=device, ?backend=backend))
    static member randint(low:int, high:int, shape:seq<int>, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.RandomInt(shape|>Seq.toArray, low, high, ?dtype=dtype, ?device=device, ?backend=backend))
    static member randint(low:int, high:int, length:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = Tensor(RawTensor.RandomInt([|length|], low, high, ?dtype=dtype, ?device=device, ?backend=backend))
    static member multinomial(probs:Tensor, numSamples:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = probs.multinomial(numSamples, ?dtype=dtype, ?device=device, ?backend=backend)
    static member multinomial(numSamples:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = fun (probs:Tensor) -> probs.multinomial(numSamples, ?dtype=dtype, ?device=device, ?backend=backend)
    static member bernoulli(probs:Tensor, ?dtype:Dtype, ?device:Device, ?backend:Backend) = probs.bernoulli(?dtype=dtype, ?device=device, ?backend=backend)
    static member bernoulli(?dtype:Dtype, ?device:Device, ?backend:Backend) = fun (probs:Tensor) -> probs.bernoulli(?dtype=dtype, ?device=device, ?backend=backend)
    static member dropout(a:Tensor, ?p:double) = a.dropout(?p=p)
    static member dropout(?p:double) = fun (a:Tensor) -> a.dropout(?p=p)
    static member dropout2d(a:Tensor, ?p:double) = a.dropout2d(?p=p)
    static member dropout2d(?p:double) = fun (a:Tensor) -> a.dropout2d(?p=p)
    static member dropout3d(a:Tensor, ?p:double) = a.dropout3d(?p=p)
    static member dropout3d(?p:double) = fun (a:Tensor) -> a.dropout3d(?p=p)
    static member zerosLike(a:Tensor, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.zerosLike(?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member zerosLike(shape:seq<int>, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.zerosLike(shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member onesLike(a:Tensor, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.onesLike(?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member onesLike(shape:seq<int>, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.onesLike(shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member fullLike(a:Tensor, value:scalar, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.fullLike(value, ?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member fullLike(value:scalar, ?shape, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.fullLike(value, ?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member arangeLike(a:Tensor, endVal:float, ?startVal:float, ?step:float, ?dtype:Dtype, ?device:Device, ?backend:Backend) = a.arangeLike(endVal=endVal, ?startVal=startVal, ?step=step, ?dtype=dtype, ?device=device, ?backend=backend)
    static member arangeLike(endVal:float, ?startVal:float, ?step:float, ?dtype:Dtype, ?device:Device, ?backend:Backend) = fun (a:Tensor) -> a.arangeLike(endVal=endVal, ?startVal=startVal, ?step=step, ?dtype=dtype, ?device=device, ?backend=backend)
    static member arangeLike(a:Tensor, endVal:int, ?startVal:int, ?step:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = a.arangeLike(endVal=endVal, ?startVal=startVal, ?step=step, ?dtype=dtype, ?device=device, ?backend=backend)
    static member arangeLike(endVal:int, ?startVal:int, ?step:int, ?dtype:Dtype, ?device:Device, ?backend:Backend) = fun (a:Tensor) -> a.arangeLike(endVal=endVal, ?startVal=startVal, ?step=step, ?dtype=dtype, ?device=device, ?backend=backend)
    static member onehotLike(a:Tensor, length:int, hot:int, ?dtype, ?device, ?backend) = a.onehotLike(length, hot, ?dtype=dtype, ?device=device, ?backend=backend)
    static member onehotLike(length:int, hot:int, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.onehotLike(length, hot, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randLike(a:Tensor, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.randLike(?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randLike(shape:seq<int>, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.randLike(shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randnLike(a:Tensor, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.randnLike(?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randnLike(shape:seq<int>, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.randnLike(shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randintLike(a:Tensor, low:int, high:int, ?shape:seq<int>, ?dtype, ?device, ?backend) = a.randintLike(low=low, high=high, ?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member randintLike(low:int, high:int, ?shape:seq<int>, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.randintLike(low=low, high=high, ?shape=shape, ?dtype=dtype, ?device=device, ?backend=backend)
    static member zeroLike(a:Tensor, ?dtype, ?device, ?backend) = a.zeroLike(?dtype=dtype, ?device=device, ?backend=backend)
    static member oneLike(a:Tensor, ?dtype, ?device, ?backend) = a.oneLike(?dtype=dtype, ?device=device, ?backend=backend)
    static member nelement(a:Tensor) = a.nelement
    static member like(a:Tensor, value:obj, ?dtype, ?device, ?backend) = a.like(value, ?dtype=dtype, ?device=device, ?backend=backend)
    static member like(value:obj, ?dtype, ?device, ?backend) = fun (a:Tensor) -> a.like(value, ?dtype=dtype, ?device=device, ?backend=backend)
    static member clone(a:Tensor) = a.clone()
    static member lt(a:Tensor, b:Tensor) = a.lt(b)
    static member lt(b:Tensor) = fun (a:Tensor) -> a.lt(b)
    static member gt(a:Tensor, b:Tensor) = a.gt(b)
    static member gt(b:Tensor) = fun (a:Tensor) -> a.gt(b)
    static member le(a:Tensor, b:Tensor) = a.le(b)
    static member le(b:Tensor) = fun (a:Tensor) -> a.le(b)
    static member ge(a:Tensor, b:Tensor) = a.ge(b)
    static member ge(b:Tensor) = fun (a:Tensor) -> a.ge(b)
    static member isinf(a:Tensor) = a.isinf()
    static member isnan(a:Tensor) = a.isnan()
    static member hasinf(a:Tensor) = a.hasinf()
    static member hasnan(a:Tensor) = a.hasnan()
    static member argmax(a:Tensor) = a.argmax()
    static member argmin(a:Tensor) = a.argmin()
    static member max(a:Tensor) = a.max()
    static member min(a:Tensor) = a.min()
    static member max(a:Tensor, b:Tensor) = a.max(b)
    static member min(a:Tensor, b:Tensor) = a.min(b)
    static member clamp(a:Tensor, ?low:scalar, ?high:scalar) = a.clamp(?low=low, ?high=high)
    static member clamp(?low:scalar, ?high:scalar) = fun (a:Tensor) -> a.clamp(?low=low, ?high=high)
    static member diagonal(a:Tensor, ?offset:int, ?dim1:int, ?dim2:int) = a.diagonal(?offset=offset, ?dim1=dim1, ?dim2=dim2)
    static member diagonal(offset:int, ?dim1:int, ?dim2:int) = fun (a:Tensor) -> a.diagonal(offset=offset, ?dim1=dim1, ?dim2=dim2)
    static member trace(a:Tensor) = a.trace()
    static member expand(a:Tensor, shape:seq<int>) = a.expand(shape)
    static member expand(shape:seq<int>) = fun (a:Tensor) -> a.expand(shape)
    static member stack(tensors:seq<Tensor>, ?dim:int) = Tensor.stack(tensors, ?dim=dim)
    static member stack(dim:int) = fun (tensors:seq<Tensor>) -> Tensor.stack(tensors, dim=dim)
    static member unstack(a:Tensor, ?dim:int) = a.unstack(?dim=dim)
    static member unstack(dim:int) = fun (a:Tensor) -> a.unstack(dim=dim)
    static member cat(tensors:seq<Tensor>, ?dim:int) = Tensor.cat(tensors, ?dim=dim)
    static member cat(dim:int) = fun (tensors:seq<Tensor>) -> Tensor.cat(tensors, dim=dim)
    static member split(a:Tensor, sizes:seq<int>, ?dim:int) = a.split(sizes, ?dim=dim)
    static member split(sizes:seq<int>, ?dim:int) = fun (a:Tensor) -> a.split(sizes, ?dim=dim)
    static member add(a:Tensor, b:Tensor) = a.add(b)
    static member add(b:Tensor) = fun (a:Tensor) -> a.add(b)
    static member sub(a:Tensor, b:Tensor) = a.sub(b)
    static member sub(b:Tensor) = fun (a:Tensor) -> a.sub(b)
    static member mul(a:Tensor, b:Tensor) = a.mul(b)
    static member mul(b:Tensor) = fun (a:Tensor) -> a.mul(b)
    static member div(a:Tensor, b:Tensor) = a.div(b)
    static member div(b:Tensor) = fun (a:Tensor) -> a.div(b)
    static member pow(a:Tensor, b:Tensor) = a.pow(b)
    static member pow(b:Tensor) = fun (a:Tensor) -> a.pow(b)
    static member matmul(a:Tensor, b:Tensor) = a.matmul(b)
    static member matmul(b:Tensor) = fun (a:Tensor) -> a.matmul(b)
    static member dot(a:Tensor, b:Tensor) = a.dot(b)
    static member dot(b:Tensor) = fun (a:Tensor) -> a.dot(b)
    static member neg(a:Tensor) = a.neg()
    static member sum(a:Tensor) = a.sum()
    static member sum(a:Tensor, dim:int, ?keepDim:bool) = a.sum(dim, ?keepDim=keepDim)
    static member sum(dim:int, ?keepDim:bool) = fun (a:Tensor) -> a.sum(dim, ?keepDim=keepDim)
    static member mean(a:Tensor) = a.mean()
    static member mean(a:Tensor, dim:int, ?keepDim:bool) = a.mean(dim, ?keepDim=keepDim)
    static member mean(dim:int, ?keepDim:bool) = fun (a:Tensor) -> a.mean(dim, ?keepDim=keepDim)
    static member variance(a:Tensor, ?unbiased:bool) = a.variance(?unbiased=unbiased)
    static member variance(a:Tensor, dim:int, ?keepDim:bool, ?unbiased:bool) = a.variance(dim, ?keepDim=keepDim, ?unbiased=unbiased)
    static member variance(dim:int, ?keepDim:bool, ?unbiased:bool) = fun (a:Tensor) -> a.variance(dim, ?keepDim=keepDim, ?unbiased=unbiased)
    static member stddev(a:Tensor, ?unbiased:bool) = a.stddev(?unbiased=unbiased)
    static member stddev(a:Tensor, dim:int, ?keepDim:bool, ?unbiased:bool) = a.stddev(dim, ?keepDim=keepDim, ?unbiased=unbiased)
    static member stddev(dim:int, ?keepDim:bool, ?unbiased:bool) = fun (a:Tensor) -> a.stddev(dim, ?keepDim=keepDim, ?unbiased=unbiased)
    static member gather(a:Tensor, dim:int, indices:Tensor) = a.gather(dim, indices)
    static member gather(dim:int, indices:Tensor) = fun (a:Tensor) -> a.gather(dim, indices)
    static member transpose(a:Tensor, dim0:int, dim1:int) = a.transpose(dim0, dim1)
    static member transpose(dim0:int, dim1:int) = fun (a:Tensor) -> a.transpose(dim0, dim1)
    static member transpose(a:Tensor) = a.transpose()
    static member squeeze(a:Tensor, ?dim:int) = a.squeeze(?dim=dim)
    static member squeeze(dim:int) = fun (a:Tensor) -> a.squeeze(dim=dim)
    static member unsqueeze(a:Tensor, dim:int) = a.unsqueeze(dim)
    static member unsqueeze(dim:int) = fun (a:Tensor) -> a.unsqueeze(dim)
    static member flip(a:Tensor, dims:seq<int>) = a.flip(dims)
    static member flip(dims:seq<int>) = fun (a:Tensor) -> a.flip(dims)
    static member dilate(a:Tensor, dilations:seq<int>) = a.dilate(dilations)
    static member dilate(dilations:seq<int>) = fun (a:Tensor) -> a.dilate(dilations)
    static member undilate(a:Tensor, dilations:seq<int>) = a.undilate(dilations)
    static member undilate(dilations:seq<int>) = fun (a:Tensor) -> a.undilate(dilations)
    static member repeat(a:Tensor, dim:int, times:int) = a.repeat(dim, times)
    static member repeat(dim:int, times:int) = fun (a:Tensor) -> a.repeat(dim, times)
    static member view(a:Tensor, shape:seq<int>) = a.view(shape)
    static member view(shape:seq<int>) = fun (a:Tensor) -> a.view(shape)
    static member view(a:Tensor, shape:int) = a.view(shape)
    static member view(shape:int) = fun (a:Tensor) -> a.view(shape)
    static member viewAs(a:Tensor, b:Tensor) = a.viewAs(b)
    static member viewAs(b:Tensor) = fun (a:Tensor) -> a.viewAs(b)
    static member flatten(a:Tensor, ?startDim:int, ?endDim:int) = a.flatten(?startDim=startDim, ?endDim=endDim)
    static member flatten(startDim:int, ?endDim:int) = fun (a:Tensor) -> a.flatten(startDim=startDim, ?endDim=endDim)
    static member sign(a:Tensor) = a.sign()
    static member floor(a:Tensor) = a.floor()
    static member ceil(a:Tensor) = a.ceil()
    static member round(a:Tensor) = a.round()
    static member abs(a:Tensor) = a.abs()
    static member relu(a:Tensor) = a.relu()
    static member leakyRelu(a:Tensor, ?negativeSlope:float) = a.leakyRelu(?negativeSlope=negativeSlope)
    static member leakyRelu(negativeSlope:float) = fun (a:Tensor) -> a.leakyRelu(negativeSlope=negativeSlope)
    static member sigmoid(a:Tensor) = a.sigmoid()
    static member softplus(a:Tensor) = a.softplus()
    static member exp(a:Tensor) = a.exp()
    static member log(a:Tensor) = a.log()
    static member log10(a:Tensor) = a.log10()
    static member sqrt(a:Tensor) = a.sqrt()
    static member sin(a:Tensor) = a.sin()
    static member cos(a:Tensor) = a.cos()
    static member tan(a:Tensor) = a.tan()
    static member sinh(a:Tensor) = a.sinh()
    static member cosh(a:Tensor) = a.cosh()
    static member tanh(a:Tensor) = a.tanh()
    static member asin(a:Tensor) = a.asin()
    static member acos(a:Tensor) = a.acos()
    static member atan(a:Tensor) = a.atan()
    static member softmax(a:Tensor, dim:int) = a.softmax(dim)
    static member softmax(dim:int) = fun (a:Tensor) -> a.softmax(dim)
    static member logsoftmax(a:Tensor, dim:int) = a.logsoftmax(dim)
    static member logsoftmax(dim:int) = fun (a:Tensor) -> a.logsoftmax(dim)
    static member logsumexp(a:Tensor, dim:int, ?keepDim:bool) = a.logsumexp(dim, ?keepDim=keepDim)
    static member logsumexp(dim:int, ?keepDim:bool) = fun (a:Tensor) -> a.logsumexp(dim, ?keepDim=keepDim)
    static member mseLoss(input:Tensor, target:Tensor, ?reduction:string) = input.mseLoss(target, ?reduction=reduction)
    static member mseLoss(target:Tensor) = fun (input:Tensor) -> input.mseLoss(target)
    static member bceLoss(input:Tensor, target:Tensor, ?weight:Tensor, ?reduction:string) = input.bceLoss(target, ?weight=weight, ?reduction=reduction)
    static member bceLoss(target:Tensor) = fun (input:Tensor) -> input.bceLoss(target)
    static member nllLoss(input:Tensor, target:Tensor, ?weight:Tensor, ?reduction:string) = input.nllLoss(target, ?weight=weight, ?reduction=reduction)
    static member nllLoss(target:Tensor) = fun (input:Tensor) -> input.nllLoss(target)
    static member crossEntropyLoss(input:Tensor, target:Tensor, ?weight:Tensor, ?reduction:string) = input.crossEntropyLoss(target, ?weight=weight, ?reduction=reduction)
    static member crossEntropyLoss(target:Tensor) = fun (input:Tensor) -> input.crossEntropyLoss(target)
    static member maxpool1d(a:Tensor, kernelSize:int, ?stride:int, ?padding:int) = a.maxpool1d(kernelSize, ?stride=stride, ?padding=padding)
    static member maxpool1d(kernelSize:int, ?stride:int, ?padding:int) = fun (a:Tensor) -> a.maxpool1d(kernelSize, ?stride=stride, ?padding=padding)
    static member maxpool1di(a:Tensor, kernelSize:int, ?stride:int, ?padding:int) = a.maxpool1di(kernelSize, ?stride=stride, ?padding=padding)
    static member maxpool2d(a:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = a.maxpool2d(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxpool2d(?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = fun (a:Tensor) -> a.maxpool2d(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxpool2di(a:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = a.maxpool2di(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxpool3d(a:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = a.maxpool3d(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxpool3d(?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = fun (a:Tensor) -> a.maxpool3d(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxpool3di(a:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>) = a.maxpool3di(?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings)
    static member maxunpool1d(a:Tensor, indices:Tensor, kernelSize:int, ?stride:int, ?padding:int, ?outputSize:seq<int>) = a.maxunpool1d(indices, kernelSize, ?stride=stride, ?padding=padding, ?outputSize=outputSize)
    static member maxunpool1d(indices:Tensor, kernelSize:int, ?stride:int, ?padding:int, ?outputSize:seq<int>) = fun (a:Tensor) -> a.maxunpool1d(indices, kernelSize, ?stride=stride, ?padding=padding, ?outputSize=outputSize)
    static member maxunpool2d(a:Tensor, indices:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>, ?outputSize:seq<int>) = a.maxunpool2d(indices, ?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings, ?outputSize=outputSize)
    static member maxunpool2d(indices:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>, ?outputSize:seq<int>) = fun (a:Tensor) -> a.maxunpool2d(indices, ?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings, ?outputSize=outputSize)
    static member maxunpool3d(a:Tensor, indices:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>, ?outputSize:seq<int>) = a.maxunpool3d(indices, ?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings, ?outputSize=outputSize)
    static member maxunpool3d(indices:Tensor, ?kernelSize:int, ?stride:int, ?padding:int, ?kernelSizes:seq<int>, ?strides:seq<int>, ?paddings:seq<int>, ?outputSize:seq<int>) = fun (a:Tensor) -> a.maxunpool3d(indices, ?kernelSize=kernelSize, ?stride=stride, ?padding=padding, ?kernelSizes=kernelSizes, ?strides=strides, ?paddings=paddings, ?outputSize=outputSize)
    static member conv1d(a:Tensor, b:Tensor, ?stride:int, ?padding:int, ?dilation:int) = a.conv1d(b, ?stride=stride, ?padding=padding, ?dilation=dilation)
    static member conv1d(b:Tensor, ?stride:int, ?padding:int, ?dilation:int) = fun (a:Tensor) -> a.conv1d(b, ?stride=stride, ?padding=padding, ?dilation=dilation)
    static member conv2d(a:Tensor, b:Tensor, ?stride:int, ?strides:seq<int>, ?padding:int, ?paddings:seq<int>, ?dilation:int, ?dilations:seq<int>) = a.conv2d(b, ?stride=stride, ?strides=strides, ?padding=padding, ?paddings=paddings, ?dilation=dilation, ?dilations=dilations)
    static member conv2d(b:Tensor, ?stride:int, ?strides:seq<int>, ?padding:int, ?paddings:seq<int>, ?dilation:int, ?dilations:seq<int>) = fun (a:Tensor) -> a.conv2d(b, ?stride=stride, ?strides=strides, ?padding=padding, ?paddings=paddings, ?dilation=dilation, ?dilations=dilations)
    static member conv3d(a:Tensor, b:Tensor, ?stride:int, ?strides:seq<int>, ?padding:int, ?paddings:seq<int>, ?dilation:int, ?dilations:seq<int>) = a.conv3d(b, ?stride=stride, ?strides=strides, ?padding=padding, ?paddings=paddings, ?dilation=dilation, ?dilations=dilations)
    static member conv3d(b:Tensor, ?stride:int, ?strides:seq<int>, ?padding:int, ?paddings:seq<int>, ?dilation:int, ?dilations:seq<int>) = fun (a:Tensor) -> a.conv3d(b, ?stride=stride, ?strides=strides, ?padding=padding, ?paddings=paddings, ?dilation=dilation, ?dilations=dilations)
    static member pad(a:Tensor, paddings:seq<int>) = a.pad(paddings)
    static member pad(paddings:seq<int>) = fun (a:Tensor) -> a.pad(paddings)
    static member cast(a:Tensor, dtype:Dtype) = a.cast(dtype)
    static member cast(dtype:Dtype) = fun (a:Tensor) -> a.cast(dtype)
    static member move(a:Tensor, ?dtype, ?device, ?backend) = a.move(?dtype=dtype, ?device=device, ?backend=backend)
    static member move(?dtype, ?device, ?backend) = fun (a:Tensor) -> a.move(?dtype=dtype, ?device=device, ?backend=backend)
    static member config(?dtype: Dtype, ?device: Device, ?backend: Backend) = 
        dtype |> Option.iter (fun d -> Dtype.Default <- d)
        device |> Option.iter (fun d -> Device.Default <- d)
        backend |> Option.iter (fun d -> Backend.Default <- d)
        dsharp.tensor(0.) |> ignore // We need this to ensure the backend assemblies are loaded and backend is ready to set the random seed immediately after config

    static member config() = Dtype.Default, Device.Default, Backend.Default
    static member config((dtype,device,backend)) = dsharp.config(dtype, device, backend)
    static member devices(?deviceType, ?backend) = BackendStatics.Get(?backend=backend).GetDevices(?deviceType=deviceType)
    static member isDeviceTypeSupported(deviceType, ?backend) = BackendStatics.Get(?backend=backend).IsDeviceTypeSupported(deviceType)
    static member isCudaSupported(?backend) = BackendStatics.Get(?backend=backend).IsDeviceTypeSupported(DeviceType.CUDA)

// Methods mirroring F# array modules
// TODO: implement more differentiable higher-order functions and corresponding unit tests for their derivatives
type dsharp with
    static member init (count:int) (initializer:int->'a) = Array.init count initializer |> dsharp.tensor
    static member init2d (length1:int) (length2:int) (initializer:int->int->'a) = Array2D.init length1 length2 initializer |> dsharp.tensor
    static member init3d (length1:int) (length2:int) (length3:int) (initializer:int->int->int->'a) = Array3D.init length1 length2 length3 initializer |> dsharp.tensor
    static member init4d (length1:int) (length2:int) (length3:int) (length4:int) (initializer:int->int->int->int->'a) = Array4D.init length1 length2 length3 length4 initializer |> dsharp.tensor
    static member create (count:int) (value:'a) = Array.create count value |> dsharp.tensor
    static member zeroCreate (count:int) = Array.zeroCreate count |> dsharp.tensor
    static member mapi (mapping:int[]->Tensor->Tensor) (tensor:Tensor) = // Differentiable map
        let tflat = tensor.view(-1)
        let items = Array.init (tflat.nelement) (fun i -> mapping (flatIndexToIndex tensor.shape i) tflat.[i])
        dsharp.stack(items).view(tensor.shape)
    static member mapi2 (mapping:int[]->Tensor->Tensor->Tensor) (tensor1:Tensor) (tensor2:Tensor) =  // Differentiable map2
        if tensor1.shape <> tensor2.shape then failwithf "Expecting tensor1.shape (%A) and tensor2.shape (%A) to be the same" tensor1.shape tensor2.shape
        let tflat1 = tensor1.view(-1)
        let tflat2 = tensor2.view(-1)
        let items = Array.init (tflat1.nelement) (fun i -> mapping (flatIndexToIndex tensor1.shape i) tflat1.[i] tflat2.[i])
        dsharp.stack(items).view(tensor1.shape)
    static member mapi3 (mapping:int[]->Tensor->Tensor->Tensor->Tensor) (tensor1:Tensor) (tensor2:Tensor) (tensor3:Tensor) =  // Differentiable map3
        if (tensor1.shape <> tensor2.shape) || (tensor2.shape <> tensor3.shape) then failwithf "Expecting tensor1.shape (%A), tensor2.shape (%A), tensor3.shape (%A) to be the same" tensor1.shape tensor2.shape tensor3.shape
        let tflat1 = tensor1.view(-1)
        let tflat2 = tensor2.view(-1)
        let tflat3 = tensor3.view(-1)
        let items = Array.init (tflat1.nelement) (fun i -> mapping (flatIndexToIndex tensor1.shape i) tflat1.[i] tflat2.[i] tflat3.[i])
        dsharp.stack(items).view(tensor1.shape)
    static member map (mapping:Tensor->Tensor) (tensor:Tensor) = tensor |> dsharp.mapi (fun _ v -> mapping v)
    static member map2 (mapping:Tensor->Tensor->Tensor) (tensor1:Tensor) (tensor2:Tensor) = dsharp.mapi2 (fun _ v1 v2 -> mapping v1 v2) tensor1 tensor2
    static member map3 (mapping:Tensor->Tensor->Tensor->Tensor) (tensor1:Tensor) (tensor2:Tensor) (tensor3:Tensor) = dsharp.mapi3 (fun _ v1 v2 v3 -> mapping v1 v2 v3) tensor1 tensor2 tensor3


// Functional automatic differentiation API
type dsharp with
    static member nest() = GlobalNestingLevel.Next() |> ignore
    static member nest(level) = GlobalNestingLevel.Set(level)
    static member nestLevel() = GlobalNestingLevel.Current
    static member nestReset() = GlobalNestingLevel.Reset()
    static member primal (tensor:Tensor) = tensor.primal
    static member derivative (tensor:Tensor) = tensor.derivative
    static member primalDerivative (tensor:Tensor) = tensor.primal, tensor.derivative
    static member forwardDiff (tag:uint32) (derivative:Tensor) (tensor:Tensor) = tensor.forwardDiff(derivative, tag)
    static member reverseDiff (tag:uint32) (tensor:Tensor) = tensor.reverseDiff(tag)
    static member reverseReset (tensor:Tensor) = tensor.reverseReset(true)
    static member reversePush (value:Tensor) (tensor:Tensor) = tensor.reversePush(value)
    static member reverse (value:Tensor) (tensor:Tensor) = tensor.reverse(value)
    static member evalForwardDiff f x v = x |> dsharp.forwardDiff (GlobalNestingLevel.Next()) v |> f |> dsharp.primalDerivative
    static member evalReverseDiff f x =
        let x = x |> dsharp.reverseDiff (GlobalNestingLevel.Next())
        let fx = f x
        let r = fun v -> fx |> dsharp.reverse v; x.derivative
        fx.primal, r
    static member evalForwardDiffs (f:Tensor->Tensor) x (v:Tensor[]) =
        let n = v.Length
        if n = 0 then [|f x|]
        else
            let mutable x = x
            for i in 0..n-1 do
                x <- x |> dsharp.forwardDiff (GlobalNestingLevel.Next()) v.[i]
            let mutable fx = f x
            [|for _ in 0..n-1 do
                let d = fx.derivativeDeep
                fx <- fx.primal
                d
                |] |> Array.rev |> Array.append [|fx|]
    static member fjacobianv f (x:Tensor) (v:Tensor) = 
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let fx, d = dsharp.evalForwardDiff f x v
        if x.dim <> 1 || fx.dim <> 1 then failwithf "f must be a vector-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        fx, d
    static member jacobianv f x v = dsharp.fjacobianv f x v |> snd
    static member fgradv f (x:Tensor) (v:Tensor) =
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let fx, d = dsharp.evalForwardDiff f x v
        if x.dim <> 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        fx, d
    static member gradv f x v = dsharp.fgradv f x v |> snd
    static member fdiff f (x:Tensor) =
        let fx, d = dsharp.evalForwardDiff f x (x.onesLike())
        if x.dim <> 0 then failwithf "f must be a function of a scalar, encountered f:%A->%A" x.shape fx.shape
        fx, d
    static member diff f x = dsharp.fdiff f x |> snd
    static member ffdiffn (n:int) (f:Tensor->Tensor) (x:Tensor) =
        if n < 0 then failwith "Differentiation order n must be >= 0"
        if x.dim <> 0 then failwithf "f must be a function of a scalar"
        dsharp.evalForwardDiffs f x (Array.create n (x.onesLike()))
    static member fdiffn n f x = let a = dsharp.ffdiffn n f x in a |> Array.head, a |> Array.last
    static member diffn n f x = dsharp.fdiffn n f x |> snd
    static member fdiff2 f x = dsharp.fdiffn 2 f x
    static member diff2 f x = dsharp.diffn 2 f x
    static member fjacobianTv f x (v:Tensor) =
        let fx, r = dsharp.evalReverseDiff f x
        if x.dim <> 1 || fx.dim <> 1 then failwithf "f must be a vector-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        if fx.nelement <> v.nelement then failwithf "(f x) and v must have the same number of elements"
        fx, r v
    static member jacobianTv f x v = dsharp.fjacobianTv f x v |> snd
    static member fjacobian (f:Tensor->Tensor) x =
        let fx, r = dsharp.evalReverseDiff f x
        if x.dim <> 1 || fx.dim <> 1 then failwithf "f must be a vector-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        if x.nelement > fx.nelement then
            fx, dsharp.stack(Array.init fx.nelement (fun i -> r (x.onehotLike(fx.nelement, i))), 0)
        else
            fx, dsharp.stack(Array.init x.nelement (fun j -> dsharp.jacobianv f x (x.onehotLike(x.nelement, j))), 1)
    static member jacobian f x = dsharp.fjacobian f x |> snd
    static member fgrad f (x:Tensor) =
        if x.dim = 0 then 
            dsharp.fdiff f x
        else
            let fx, r = dsharp.evalReverseDiff f x
            if x.dim > 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector or scalar, encountered f:%A->%A" x.shape fx.shape
            fx, r (fx.onesLike())
    static member grad f x = dsharp.fgrad f x |> snd
    static member fgradhessianv f (x:Tensor) (v:Tensor) =
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let x = x |> dsharp.reverseDiff (GlobalNestingLevel.Next())
        let fx, gv = dsharp.fgradv f x v
        gv.reverse()
        fx.primal, gv.primal, x.derivative
    static member gradhessianv f x v = let _, gv, hv = dsharp.fgradhessianv f x v in gv, hv
    static member fhessianv f x v = let fx, _, hv = dsharp.fgradhessianv f x v in fx, hv
    static member hessianv f x v = dsharp.fhessianv f x v |> snd
    static member fgradhessian (f:Tensor->Tensor) (x:Tensor) =
        let mutable fx = dsharp.zero()
        let gvs, hvs = Array.init x.nelement (fun j -> let ffxx, gv, hv = dsharp.fgradhessianv f x (x.onehotLike(x.nelement, j)) in fx <- ffxx; gv, hv) |> Array.unzip
        let h = dsharp.stack(hvs, 1)
        let g = dsharp.stack(gvs)
        if x.dim <> 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        fx, g, h
    static member gradhessian f x = let _, g, h = dsharp.fgradhessian f x in g, h
    static member fhessian (f:Tensor->Tensor) (x:Tensor) =
        let mutable fx = dsharp.zero()
        let h = dsharp.stack(Array.init x.nelement (fun j -> let ffxx, hv = dsharp.fhessianv f x (x.onehotLike(x.nelement, j)) in fx <- ffxx; hv), 1)
        if x.dim <> 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        fx, h
    static member hessian f x = dsharp.fhessian f x |> snd
    static member flaplacian f x =
        let fx, h = dsharp.fhessian f x
        fx, h.trace()
    static member laplacian f x = dsharp.flaplacian f x |> snd
    static member fcurl f x =
        let fx, j = dsharp.fjacobian f x
        if j.shape <> [|3; 3|] then failwithf "f must be a function with a three-by-three Jacobian"
        fx, dsharp.stack([j.[2, 1] - j.[1, 2]; j.[0, 2] - j.[2, 0]; j.[1, 0] - j.[0, 1]])
    static member curl f x = dsharp.fcurl f x |> snd
    static member fdivergence f x =
        let fx, j = dsharp.fjacobian f x
        if j.shape.[0] <> j.shape.[1] then failwithf "f must have a square Jacobian"
        fx, j.trace()
    static member divergence f x = dsharp.fdivergence f x |> snd
    static member fcurldivergence f x =
        let fx, j = dsharp.fjacobian f x
        if j.shape <> [|3; 3|] then failwithf "f must be a function with a three-by-three Jacobian"
        fx, dsharp.stack([j.[2, 1] - j.[1, 2]; j.[0, 2] - j.[2, 0]; j.[1, 0] - j.[0, 1]]), j.trace()
    static member curldivergence f x = let _, c, d = dsharp.fcurldivergence f x in c, d


// Functional automatic differentiation API shorthand names
type dsharp with
    static member gvp f x v = dsharp.gradv f x v
    static member g f x = dsharp.grad f x
    static member hvp f x v = dsharp.hessianv f x v
    static member h f x = dsharp.hessian f x
    static member gh f x = dsharp.gradhessian f x
    static member ghvp f x v = dsharp.gradhessianv f x v
    static member jvp f x v = dsharp.jacobianv f x v
    static member vjp f x v = dsharp.jacobianTv f x v
    static member j f x = dsharp.jacobian f x
    static member fgvp f x v = dsharp.fgradv f x v
    static member fg f x = dsharp.fgrad f x
    static member fgh f x = dsharp.fgradhessian f x
    static member fhvp f x v = dsharp.fhessianv f x v
    static member fh f x = dsharp.fhessian f x
    static member fghvp f x v = dsharp.fgradhessianv f x v
    static member fjvp f x v = dsharp.fjacobianv f x v
    static member fvjp f x v = dsharp.fjacobianTv f x v
    static member fj f x = dsharp.fjacobian f x    


// Functional numerical differentiation API
type dsharp with
    static member numdiff (epsilon:float) (f:Tensor->Tensor) (x:Tensor) = 
        if x.dim <> 0 then failwithf "f must be a function of a scalar"
        ((f (x + epsilon)) - (f (x - epsilon))) / (2.*epsilon)
    static member numfdiff epsilon f x = f x, dsharp.numdiff epsilon f x
    static member numfdiff2 (epsilon:float) (f:Tensor->Tensor) (x:Tensor) =
        if x.dim <> 0 then failwithf "f must be a function of a scalar"
        let fx = f x
        fx, ((f (x + epsilon)) - 2. * fx + (f (x - epsilon))) / (epsilon * epsilon)
    static member numdiff2 epsilon f x = dsharp.numfdiff2 epsilon f x |> snd
    static member numjacobianv (epsilon:float) (f:Tensor->Tensor) (x:Tensor) (v:Tensor) =
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let veps = v * epsilon
        let fxa, fxb = f (x+veps), f (x-veps)
        if x.dim <> 1 || fxa.dim <> 1 then failwithf "f must be a vector-valued function of a vector, encountered f:%A->%A" x.shape fxa.shape
        (fxa - fxb) / (2.*epsilon)
    static member numfjacobianv epsilon f x v = f x, dsharp.numjacobianv epsilon f x v
    static member numfjacobian (epsilon:float) (f:Tensor->Tensor) (x:Tensor) =
        let fx = f x
        if x.dim <> 1 || fx.dim <> 1 then failwithf "f must be a vector-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        let j = fx.expand([x.nelement; fx.nelement])
        let jj = dsharp.stack(Array.init x.nelement (fun i -> f (x + dsharp.onehot(x.nelement, i)*epsilon)))
        fx, (jj - j).transpose() / epsilon
    static member numjacobian epsilon f x = dsharp.numfjacobian epsilon f x |> snd
    static member numgradv (epsilon:float) (f:Tensor->Tensor) (x:Tensor) (v:Tensor) =
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let veps = v * epsilon
        let fxa, fxb = f (x + veps), f (x - veps)
        if x.dim <> 1 || fxa.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fxa.shape
        (fxa - fxb) / (2.*epsilon)
    static member numfgradv epsilon f x v = f x, dsharp.numgradv epsilon f x v
    static member numfgrad (epsilon:float) (f:Tensor->Tensor) (x:Tensor) =
        if x.dim = 0 then
            dsharp.numfdiff epsilon f x
        else
            let fx = f x
            if x.dim > 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector or scalar, encountered f:%A->%A" x.shape fx.shape
            let gg = dsharp.stack(Array.init x.nelement (fun i -> let h = dsharp.onehot(x.nelement, i)*epsilon in f (x + h) - f (x - h)))
            fx, gg/(2.*epsilon)
    static member numgrad epsilon f x = dsharp.numfgrad epsilon f x |> snd
    static member numfgradhessian (epsilon:float) (f:Tensor->Tensor) (x:Tensor) =
        let fx, g = dsharp.numfgrad epsilon f x
        if x.dim <> 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        let h = g.expand([x.nelement; x.nelement])
        let hh = dsharp.stack(Array.init x.nelement (fun i -> dsharp.numgrad epsilon f (x + dsharp.onehot(x.nelement, i)*epsilon)))
        fx, g, (hh - h) / epsilon
    static member numgradhessian epsilon f x = let _, g, h = dsharp.numfgradhessian epsilon f x in g, h
    static member numfhessian epsilon f x = let fx, _, h = dsharp.numfgradhessian epsilon f x in fx, h
    static member numhessian epsilon f x = dsharp.numfhessian epsilon f x |> snd
    static member numfhessianv (epsilon:float) (f:Tensor->Tensor) (x:Tensor) (v:Tensor) =
        if x.nelement <> v.nelement then failwithf "x and v must have the same number of elements"
        let veps = v*epsilon
        let fx, g = dsharp.numfgrad epsilon f x
        if x.dim <> 1 || fx.dim <> 0 then failwithf "f must be a scalar-valued function of a vector, encountered f:%A->%A" x.shape fx.shape
        let gg = dsharp.numgrad epsilon f (x + veps)
        fx, (gg-g)/epsilon
    static member numhessianv epsilon f x v = dsharp.numfhessianv epsilon f x v |> snd
    static member numflaplacian epsilon f x =
        let fx, h = dsharp.numfhessian epsilon f x
        fx, h.trace()
    static member numlaplacian epsilon f x = dsharp.numflaplacian epsilon f x |> snd
    static member numfcurl epsilon f x =
        let fx, j = dsharp.numfjacobian epsilon f x
        if j.shape <> [|3; 3|] then failwithf "f must be a function with a three-by-three Jacobian"
        fx, dsharp.stack([j.[2, 1] - j.[1, 2]; j.[0, 2] - j.[2, 0]; j.[1, 0] - j.[0, 1]])
    static member numcurl epsilon f x = dsharp.numfcurl epsilon f x |> snd
    static member numfdivergence epsilon f x =
        let fx, j = dsharp.numfjacobian epsilon f x
        if j.shape.[0] <> j.shape.[1] then failwithf "f must have a square Jacobian"
        fx, j.trace()
    static member numdivergence epsilon f x = dsharp.numfdivergence epsilon f x |> snd
    static member numfcurldivergence epsilon f x =
        let fx, j = dsharp.numfjacobian epsilon f x
        if j.shape <> [|3; 3|] then failwithf "f must be a function with a three-by-three Jacobian"
        fx, dsharp.stack([j.[2, 1] - j.[1, 2]; j.[0, 2] - j.[2, 0]; j.[1, 0] - j.[0, 1]]), j.trace()
    static member numcurldivergence epsilon f x = let _, c, d = dsharp.numfcurldivergence epsilon f x in c, d


// Functional numerical differentiation API shorthand names
type dsharp with
    static member numgvp f x v = dsharp.numgradv f x v
    static member numg f x = dsharp.numgrad f x
    static member numhvp f x v = dsharp.numhessianv f x v
    static member numh f x = dsharp.numhessian f x
    static member numgh f x = dsharp.numgradhessian f x
    static member numjvp f x v = dsharp.numjacobianv f x v
    static member numj f x = dsharp.numjacobian f x
    static member numfgvp f x v = dsharp.numfgradv f x v
    static member numfg f x = dsharp.numfgrad f x
    static member numfhvp f x v = dsharp.numfhessianv f x v
    static member numfh f x = dsharp.numfhessian f x
    static member numfgh f x = dsharp.numfgradhessian f x
    static member numfjvp f x v = dsharp.numfjacobianv f x v
    static member numfj f x = dsharp.numfjacobian f x    
