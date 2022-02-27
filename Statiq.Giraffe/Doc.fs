namespace Statiq.Giraffe

module Doc =
    open Statiq.Common
    open Statiq.Giraffe.Patterns
    open System
    module private Key =
        let rec Next = nameof Next
        let rec Previous = nameof Previous
    open Giraffe.ViewEngine
    let hyperLink (doc: IDocument) attr  =
        a [_href (doc.GetLink()); yield! attr]
    let getLead (doc: IDocument) =        
        doc.Get<string>("Lead")
        |> Option.ofObj
    
    let getTitle (doc: IDocument) =
        doc.Get<string>("Title")
        |> Option.ofObj
    
    let getExcerpt (doc: IDocument)=
        doc.Get<string>("Excerpt")
        |> Option.ofObj
        
    let getCreated (doc: IDocument) =
        doc.Get<Nullable<DateTime>>("Published")
        |> Option.ofNullable
    let getAuthor (doc: IDocument) =
        doc.Get<string>("Author")
        |> Option.ofObj
    let next (doc: IDocument) =
        let (NullOption nxt) = doc.GetDocument Key.Next
        nxt
    let previous (doc: IDocument) =
        let (NullOption prev) = doc.GetDocument Key.Previous
        prev

