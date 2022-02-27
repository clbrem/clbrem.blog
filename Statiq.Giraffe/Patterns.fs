namespace Statiq.Giraffe

module Patterns =
    let (|NullOption|) item =
        if item = null then None
        else Some item

