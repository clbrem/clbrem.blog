namespace Statiq.Giraffe
open System
open Giraffe.ViewEngine
open Statiq.Common
open System.Threading.Tasks


type RenderTemplate private ( template: IDocument * IExecutionContext -> XmlNode,
                              condition: IDocument * IExecutionContext -> bool
                            ) =  
  inherit SyncModule() with
    new(template: IDocument * IExecutionContext -> XmlNode) =
      RenderTemplate(template, function | _ -> true)
    new(template: IDocument -> XmlNode) =
      RenderTemplate(fun (doc, _) -> template doc)
    new(template: IExecutionContext-> XmlNode) =
      RenderTemplate(fun (_, context) -> template context)
    member _.When(condition:  IDocument * IExecutionContext -> bool) =
      RenderTemplate(template, condition)
    member _.When(condition:  IDocument -> bool) =
      RenderTemplate(template, fun (doc, _) -> condition doc)
    member _.When(condition:  IExecutionContext -> bool) =
      RenderTemplate(template, fun (_, ctx) -> condition ctx)
    member _.When(config: bool) =
      RenderTemplate(template, fun _ -> config)
    override _.ExecuteInput(input: IDocument, context: IExecutionContext) =
      if condition(input, context) then
        let rendered =
          template (input, context) |> RenderView.AsString.htmlNode
            |> sprintf "<!DOCTYPE html> %s %s" Environment.NewLine
        input.Clone(
            context.GetContentProvider(rendered, MediaTypes.Html)
        ).Yield()
      else
        input.Yield()

type RenderTemplateAsync private (template: IDocument * IExecutionContext -> Task<XmlNode>,
                                  condition: IDocument * IExecutionContext -> bool) =  
  inherit ParallelModule() with
    new(template: IDocument * IExecutionContext -> Task<XmlNode>) =
      RenderTemplateAsync(template, function | _ -> true)
    new(template: IDocument -> Task<XmlNode>) =
      RenderTemplateAsync(fun (doc, _) -> template doc)
    new(template: IExecutionContext-> Task<XmlNode>) =
      RenderTemplateAsync(fun (_, context) -> template context)
    member _.When(condition:  IDocument * IExecutionContext -> bool) =
      RenderTemplateAsync(template, condition)
    member _.When(condition:  IDocument -> bool) =
      RenderTemplateAsync(template, fun (doc, _) -> condition doc)
    member _.When(condition:  IExecutionContext -> bool) =
      RenderTemplateAsync(template, fun (_, ctx) -> condition ctx)
    member _.When(config: bool) =
      RenderTemplateAsync(template, fun _ -> config)      
    override _.ExecuteInputAsync(input: IDocument, context: IExecutionContext) =
      task {
        if condition(input, context) then
          let! rendered = template (input, context)
          let view =
            rendered |> RenderView.AsString.htmlNode
            |> sprintf "<!DOCTYPE html> %s %s" Environment.NewLine          
          return input.Clone(
                    context.GetContentProvider(view, MediaTypes.Html)
                  ).Yield()
        else
          return input.Yield()
      }
       

