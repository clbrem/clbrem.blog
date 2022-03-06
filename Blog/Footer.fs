namespace Blog
open Statiq.Common
open Giraffe.ViewEngine
open Statiq.Giraffe
open Statiq.Giraffe.ComputationExpressions

module Footer =
    module Css =
        let rec footer = nameof footer
        

    let create (doc: IDocument, ctx: IExecutionContext) attributes content=
        let today = System.DateTime.Now.Year |> sprintf "%i"
        let home =
            ctx.OutputPages.["index.html"].FirstOrDefaultDestination()
        let homeLink =
            home
            |> Doc.hyperLink
        let homeTitle =
            home |> Doc.getTitle |> Option.defaultValue "Author"
        footer ( attrs{_class Css.footer; yield! attributes}) [
            p [ ] [
                yield Text $"&copy; {today} "
                yield homeLink [] [str homeTitle]
            ]
            p [] [
                yield Text "Powered by "
                yield a [_href "https://www.statiq.dev"] [str "Statiq"]                
            ]
            p [] [
                yield Text "Theme adapted from "
                yield a [_href "https://github.com/nanxiaobei/hugo-paper"] [str "Paper 5.1"]
            ]
            yield! content
        ]
