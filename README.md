# Falsify
Falsify is a body of methods designed to efficiently reject bad data.

A falsifier must prove that some given data does _not_ conform to a given specification, using little memory and little time. Failure to falsify does _not_ indicate the data is valid, but a successful falsification means the data can be rejected without further evaluation.

For example, given the claim "this data is PNG," a PNG falsifier must prove the claim is wrong without wholly loading the file into memory or decoding its data. For example, one cheap technique to prove a given file is not a PNG is to show it lacks the magic bytes. Other techniques can be added, provided that they are cheap.

## The interface

Falsify.Core carries a single interface, written like so:

	interface IFalsify {
		bool Falsify(Stream stream);
		Task<bool> FalsifyAsync(Stream stream);
	}

* An ideal implementation is thread-safe.

* An ideal implementation returns _false_ as soon as it is able to show the data does not meet its specification.

* An ideal implementation reads as little of the stream as possible.

* An ideal implementation does not keep large buffers.
