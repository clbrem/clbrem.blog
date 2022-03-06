namespace Blog
open System.IO
open Giraffe.ViewEngine
open Statiq.Common


    
module Layout =

    let linkStyle (styleLink: string) (context: IExecutionContext) =
        let myLink = context.GetLink(styleLink)        
        link [ _rel "stylesheet"; _href myLink ]
    let linkScript (scriptLink:string) (context: IExecutionContext) =
        let myLink = context.GetLink(scriptLink)
        script [ _src myLink ]     
    let defaultHead (doc: IDocument, context: IExecutionContext) attrs content =
        doc.
        let index = context.OutputPages.["index.html"].FirstOrDefaultDestination()
        head [yield! attrs] [
              meta [ _charset "utf-8" ] 
              meta [_httpEquiv "X-UA-Compatible"; _content "IE=edge"]
              meta [ _name "viewport"; _content "width=device-width, initial-scale=1, shrink-to-fit=no"]
              meta [_name "author"; _content (index.Get<string>("Author"))]
              meta [_name "description"; _content (index.Get<string>("Summary")) ]
              linkStyle "/assets/styles.css" context
              linkStyle "/assets/paper.css" context
              yield! content
        ]
    let defaultBody attributes  =
        body [attr "data-menu" "true"; yield! attributes] 
    let layout (doc: IDocument, context: IExecutionContext) =
        fun _header _body ->
            html [_lang "en"] [
                yield defaultHead (doc, context) [] _header
                yield! _body
            ]
    let layoutRaw headers body (doc: IDocument, context : IExecutionContext) =        
        task {
            use stream = doc.GetContentStream()
            use reader = new StreamReader(stream)
            let! output = reader.ReadToEndAsync()
            return [yield HtmlElements.rawText output; yield! body] |> layout (doc, context) headers
        }
        
    let prismHead =  
       [ link [_rel "stylesheet"; _href "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/themes/prism.css"]         
       ]
    let prismBody =
        [
            script [_src "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/components/prism-core.min.js"] []
            script [_src "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/plugins/autoloader/prism-autoloader.min.js"] []
        ]
            

