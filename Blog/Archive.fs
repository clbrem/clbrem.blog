namespace Blog
open Giraffe.ViewEngine
open Statiq.Common
open Statiq.Giraffe
module Archive =
    module Css =
      let rec postEntry = nameof postEntry |> Naming.dashCase
    
    let entry doc attributes content=
        article
            [_class Css.postEntry; yield! attributes]
            [
                h2 [] [
                    Doc.getTitle doc
                    |> Option.defaultValue "Post"
                    |> str
                ]
                match Doc.getCreatedFormat "yyyy-MM-dd" doc with
                | Some created -> time [] [str created]
                | None -> yield! []
                Doc.hyperLink doc [] []
                yield! content
            ]
    let create (doc: IDocument, ctx: IExecutionContext) =        
        let posts = 
            doc.GetChildren()
            |> Seq.map (fun child -> entry child [] [])
            |> List.ofSeq
        Base.create(doc, ctx) [] posts
         

                  
          