KiriLib.Variant
===================

_Types_:
--------------

### Option\<T>
  Either a T or nothing. 

  #### _factory_:
  - `Option<T>.Some(T) -> Option<T>`
  - `Option<T>.None() -> Option<T>`
  - `Option.Some<T>(T) -> Option<T>`
  - `Option.None() -> ? -> Option<T> `
  
  > Notice about _factory_: 
  > - - - - - - - - - - - - - -
  > - You may recognize the pattern `Foo<T>.Bar(T)` and `Foo.Bar<T>(T)`, the latter is 
  >   equivalent to the former, but \<T> can be abbreviated if it can be inferred by csharp.
  > - Similarly, you may also recognize something like 
  >   `Foo<.., T>.Bar() -> Foo<.., T>` and `Foo.Bar<..>() -> ? -> Foo<.., T>`, the difference 
  >   being that since the required T is not provided by the latter, it returns a intermediate 
  >   partial type that can be implicitly casted to Foo, if T and Foo can be inferred.

--------------------------------------------------------------------------------------------------

### Result\<T, E>
  Either a T indicating an ok result, or an E indicating an error.  

  #### _factory_:
  - `Result<T, E>.Ok(T) -> Result<T, E>`
  - `Result<T, E>.Err(E) -> Result<T, E>`
  - `Result.Ok<T, E>(T) -> Result<T, E>`
  - `Result.Err<T, E>(E) -> Result<T, E>`
  - `Result.Ok<T>(T) -> ? -> Result<T, E>`
  - `Result.Err<E>(E) -> ? -> Result<T, E>`

--------------------------------------------------------------------------------------------------

### ResultOk\<T>
  > Equivalent to Rust's Result\<T, ()>. 
  > An unique type is used because C# does not have zero-sized types  

  #### _factory_:
  - `ResultOk<T>.Ok(T) -> ResultOk<T>`
  - `ResultOk<T>.Err() -> ResultOk<T>`
  - `Result.Ok<T>(T) -> ? -> ResultOk<T>`
  - `Result.Err() -> ? -> ResultOk<T>`
  
  Either a T indicating an ok result, or nothing indicating an error.

  Basically Option\<T> with extra semantic meaning.

--------------------------------------------------------------------------------------------------

### ResultErr\<E>
  > Equivalent to Rust's Result\<(), E>. 

  Either nothing indicating an ok result, or an E indicating an error.

  Basically Option\<E> with extra semantic meaning.

  #### _factory_:
  - `ResultErr<E>.Ok() -> ResultErr<E>`
  - `ResultErr<E>.Err(E) -> ResultErr<E>`
  - `Result.Ok() -> ? -> ResultErr<E>`
  - `Result.Err<E>(E) -> ? -> ResultErr<E>`

_Types async_
-------------------

All the above mensioned types has an Async counterpart, that are essentially a wrapper around 
their `Task<Self>`, with all the monad methods. These cannot be constructed

_Method_: Map
-------------------