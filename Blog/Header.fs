namespace Blog
open Giraffe.ViewEngine
open Statiq.Common
open Statiq.Giraffe
open Statiq.Giraffe.ComputationExpressions
open Statiq.Giraffe.Naming
open Statiq.Giraffe.Patterns
module Header =
  module Css =
      let rec header = nameof header
      let rec logo = nameof logo
      let rec siteName = nameof siteName |> dashCase
      let rec social = nameof social
  let forkMe =
        a [_href "https://github.com/clbrem/clbrem.blog"]
            [
                  img [attr "loading" "lazy"; _width "90"; _height "90"
                       _class "attachment-full size-full fork-me"
                       _alt "Fork me on GitHub" 
                       _src "https://github.blog/wp-content/uploads/2008/12/forkme_left_white_ffffff.png?resize=90%2C90"
                       attr "data-recalc-dims" "1"                       
                      ]
            ]
  let tryGetLink defaultValue =
        function
            | Some (doc: IDocument) -> Doc.hyperLink doc 
            | None -> fun attr -> (_href defaultValue ) :: attr |> a
  let tryGetTitle defaultValue =
        Option.bind Doc.getTitle
        >> Option.defaultValue defaultValue        
  let logo attr =
      p (attrs {yield _class Css.logo ; yield! attr})
  let siteName (ctx: IExecutionContext) (_: IDocument) =
      let (NullOption index) = ctx.OutputPages.["index.html"].FirstOrDefaultDestination()
      tryGetLink "" index [_class Css.siteName;]
          [
             str (tryGetTitle "Main" index)
          ]
  let siteHeader attr =
      header (attrs {yield _class Css.header; yield! attr})
  
      
  let rec twitter attr=
      attrs{
          _class (nameof twitter)
          _style $"--url:url(../images/{nameof twitter}.svg)"
          _href $"https://twitter.com/clbrem"
          _target "_blank"
          yield! attr
      } |> a
  let rec linkedin attr =
      attrs{
          _class (nameof linkedin)
          _style $"--url:url(../images/{nameof linkedin}.svg)"
          _href $"https://linkedin.com/in/cbremer4"
          _target "_blank"
          yield! attr
      } |> a
  let rec github attr=
      attrs{
          _class (nameof github)
          _style $"--url:url(../images/{nameof github}.svg)"
          _href $"https://github.com/clbrem"
          _target "_blank"
          yield! attr
      } |> a
  let rec menu attr  =
      attrs{ _class (nameof menu); yield! attr} |> nav
      
      
  let social attr =
      attrs {
          _class Css.social
          yield! attr
      } |> nav
      
  

  let headContent (doc: IDocument, ctx: IExecutionContext) attr content =
          let (NullOption about) = ctx.OutputPages["about.html"].FirstOrDefaultDestination()          
          let url = tryGetLink "" about [] [tryGetTitle "" about |> str]           
          siteHeader attr [
              logo [] [
                  siteName ctx doc                   
              ]
              menu [] [ url ]
              social [] [
                  github [] []
                  twitter [] []
                  linkedin [] []
              ]
              
              yield! content
          ]
  