namespace Blog
open Giraffe.ViewEngine
open Statiq.Common
open Statiq.Giraffe
module Article =
    module Css =
        let rec postSingle = nameof postSingle |> Naming.dashCase        
        let rec postTitle = nameof postTitle |> Naming.dashCase
        let rec postContent = nameof postContent |> Naming.dashCase        
        let rec postTags = nameof postTags |> Naming.dashCase
        let rec postNav = nameof postNav |> Naming.dashCase
        let rec main = nameof main
        
        
        

    let metaData doc attr content =
        
        header [ _class Css.postTitle; yield! attr] [
            p  [] [
                match  Doc.getCreated doc with
                | Some date -> yield time [] [str (date.ToString("yyyy-MM-dd"))]
                | None -> yield! []
                match Doc.getAuthor doc with
                | Some author -> yield span []  [str author]
                | None -> yield! []                
            ]
            h1 [] [Doc.getTitle doc |> Option.defaultValue "Post" |> str]
             
            
            
        ] 
    let content (doc: IDocument) attr cont=
            task {
                use reader = doc.GetContentTextReader()
                let! txt = reader.ReadToEndAsync()
                return
                  section
                      [_class Css.postContent; yield! attr ]
                      [ rawText txt ; yield! cont]                   
            }
            
    let create (doc: IDocument, ctx: IExecutionContext) =
        task {
            let! inner = content doc [] []
            return
              Base.create(doc, ctx) [] [              
                article [_class Css.postSingle] [
                  metaData doc [] []
                  inner
                 ]                      
              ]                
        }