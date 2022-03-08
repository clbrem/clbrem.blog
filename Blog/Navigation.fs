namespace Blog
open Statiq.Giraffe.ComputationExpressions
open Statiq.Giraffe
open Giraffe.ViewEngine
open Statiq.Common

module Navigation =
    module Css =
        let rec mainNav = nameof mainNav |> Naming.dashCase
        let rec prev = nameof prev
        let rec next = nameof next
        
    let buttonPrev doc =
        match Doc.previous doc with
        | Some prv ->
            [
              Doc.hyperLink prv [_class Css.prev] [
                str "Previous"
              ]              
            ]
        | None -> []        
    let buttonNext doc =
        match Doc.next doc with
        | Some nxt ->
            [
              Doc.hyperLink nxt [_class Css.next] [
                str "Next"
              ]              
            ]
        | None -> []
        
    let create (doc: IDocument, _: IExecutionContext) attr content=
        nav (attrs{_class Css.mainNav; yield! attr} ) [
            yield! buttonPrev doc
            yield! buttonNext doc
            yield! content
        ]
        
        


