namespace Blog
open Statiq.Common
open Giraffe.ViewEngine
open Statiq.Giraffe.ComputationExpressions
module Base =
    module Css =
      let rec main = nameof main
    let create (doc: IDocument,ctx: IExecutionContext) attributes content=
        Layout.defaultBody []
         [
          Header.headContent (doc, ctx) [] []
          main (attrs{_class Css.main; yield! attributes}) content
          Footer.create (doc, ctx) [] []
         ]

