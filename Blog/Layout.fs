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
    let defaultHead (context: IExecutionContext) attrs content =        
        head [yield! attrs] [
            linkStyle "/assets/styles.css" context
            yield! content
        ]
    let defaultBody attributes  =
        body [attr "data-menu" "true"; yield! attributes] 
    let layout (context: IExecutionContext) =
        fun _header _body ->
            html [_lang "en"] [
                yield defaultHead context [] _header
                yield defaultBody [] _body
            ]
    let layoutRaw headers body (doc: IDocument, context : IExecutionContext) =
        
        task {
            use stream = doc.GetContentStream()
            use reader = new StreamReader(stream)
            let! output = reader.ReadToEndAsync()
            return [yield HtmlElements.rawText output; yield! body] |> layout context headers
        }
        
    let prismHead =  
       [ link [_rel "stylesheet"; _href "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/themes/prism-okaidia.css"]         
       ]
    let prismBody =
        [
            script [_src "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/components/prism-core.min.js"] []
            script [_src "https://cdn.jsdelivr.net/npm/prismjs@1.19.0/plugins/autoloader/prism-autoloader.min.js"] []
        ]
            

