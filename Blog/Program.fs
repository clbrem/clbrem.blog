module Main
open Statiq.App
open Statiq.Core
open Statiq.Web
open Statiq.Images
open Statiq.Giraffe
open Blog
open Blog.Shortcode
open System.Threading.Tasks

[<EntryPoint>]
let main args =
  Bootstrapper
        .Factory        
        .CreateWeb(args)
        .AddShortcode<Prism.Fsharp>("FSharp")
        .AddHostingCommands()
        .AddPipeline(
            "Images",          
             ReadFiles("images/*").Where(
                 fun s ->
                     List.contains s.Path.Extension [ ".jpg"; ".jpeg"; ".gif"; ".png"]
                     |> Task.FromResult
                 ),             
             MutateImage()
                 .Resize(300,400)                 
                 .OutputAsJpeg(),                 
             WriteFiles()
          )
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
        