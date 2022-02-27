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
  let tryGetLink defaultValue =
        function
            | Some (doc: IDocument) -> Doc.hyperLink doc 
            | None -> fun attr -> (_href defaultValue ) :: attr |> a
  let tryGetTitle defaultValue =
        Option.bind Doc.getTitle
        >> Option.defaultValue defaultValue        
  let logo attr =
      h1 (attrs {yield _class Css.logo ; yield! attr})
  let siteName (ctx: IExecutionContext) (doc: IDocument) attr content=
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
  let social attr =
      attrs {
          _class Css.social
          yield! attr
      } |> nav
      
  

  let content (doc: IDocument, ctx: IExecutionContext)  =
          siteHeader [] [
              logo [] [
                  siteName ctx doc [] []
                  social [] []
              ]
              social [] [
                  github [] []
                  twitter [] []
                  linkedin [] []
              ]
          ]