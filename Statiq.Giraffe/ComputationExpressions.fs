namespace Statiq.Giraffe
open Giraffe.ViewEngine
type AttributeComputationExpression() =
    
    let mergeClass a =
        let classes, acc =
            List.fold (
              fun (classes, acc) curr ->
                  match classes, curr with
                  | Some coll, KeyValue("class", item) ->
                      (Some (sprintf "%s %s" coll item), acc)
                  | None, KeyValue("class", item) ->
                      (Some item, acc)
                  | _,other ->
                      (classes, other :: acc)
               )
              (None, [])
              a
        [
         yield! match classes with | Some cls ->[KeyValue("class", cls)] | _ -> []
         yield! acc
        ]
    member _.Yield(item) =
        [item]
    member _.YieldFrom(items) = List.ofSeq items
    member _.Bind(items, f) =
        [yield! List.map f items]
    member _.Zero() = []
    member _.For(items, f) =
        List.concat (Seq.map f items)
    
    member _.Run(items: XmlAttribute list) =
        mergeClass items
    member _.Combine(a, b) =
        List.append a b
    member _.Delay(f) = f()

module ComputationExpressions =
    let attrs = AttributeComputationExpression()