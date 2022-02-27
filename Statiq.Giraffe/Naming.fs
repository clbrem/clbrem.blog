namespace Statiq.Giraffe
module Naming =
    open System.Text.RegularExpressions
    let dashCase input = Regex.Replace(input , @"(?<!^)(?<!-)((?<=\p{Ll})\p{Lu}|\p{Lu}(?=\p{Ll}))", "-$1").ToLower();

