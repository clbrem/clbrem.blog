module Main
open System.Threading.Tasks
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
                 RenderTemplate Header.content,
                 RenderTemplateAsync( Layout.layoutRaw Layout.prismHead Layout.prismBody )
                 
                 )
             )
        
        .RunAsync()
        .GetAwaiter()
        .GetResult()
        