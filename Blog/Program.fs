module Main
open Statiq.App
open Statiq.Web
open Statiq.Giraffe
open Blog
open Blog.Shortcode

[<EntryPoint>]
let main args =
  Bootstrapper
        .Factory        
        .CreateWeb(args)
        .AddShortcode<Prism.Fsharp>("FSharp")
        .AddHostingCommands()        
        .ModifyPipeline(
         "Content",
         fun content ->
             content.PostProcessModules.Add(
                 RenderTemplateAsync Article.create, 
                 RenderTemplateAsync( Layout.layoutRaw Layout.prismHead Layout.prismBody )                 
                 )
             )
        .ModifyPipeline(
          "Archives",
          fun archive ->
            archive.PostProcessModules.Add(
                 RenderTemplate Archive.create, 
                 RenderTemplateAsync( Layout.layoutRaw Layout.prismHead Layout.prismBody )
                )
            )        
        .RunAsync()
        .GetAwaiter()
        .GetResult()
        