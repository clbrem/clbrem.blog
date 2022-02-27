namespace Blog.Shortcode 
open System.Text.RegularExpressions
open System
open System.Collections.Generic
open Statiq.Common
open Giraffe.ViewEngine

type Prism (formatter: string -> string) =
    inherit SyncShortcode() with
    override this.Execute(args, content, document, context) =        
        ShortcodeResult( formatter content)

module Prism =
    let language languageName input=
        Regex("\<code\>").Replace(input, $"<code class=\"language-{languageName}\">")
        
    let rec fsharp = nameof fsharp |> language
    type Fsharp() =
        inherit Prism(fsharp) 
        
        
    

