using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Rendering
{
    public static class RenderTreeBuilderExtensions
    {
        public static FluentRenderTreeBuilder ToFluent(this RenderTreeBuilder builder)
        {
            return new(builder);
        }

        public static RenderTreeBuilder CreateTableToolbarButton<TItem>(this RenderTreeBuilder builder,
            string buttonText, Func<Task> onClick, string icon, Color color)
        {
            builder.ToFluent()
                .OpenComponent(typeof(TableToolbarButton<TItem>))
                .AddAttribute(nameof(Color), color)
                .AddAttribute(nameof(TableToolbarButton<TItem>.Icon), icon)
                .AddAttribute(nameof(TableToolbarButton<TItem>.OnClick), onClick)
                .AddAttribute(nameof(TableToolbarButton<TItem>.Text), buttonText)
                .Close();
            return builder;
        }
    }

    public class FluentRenderTreeBuilder
    {
        public FluentRenderTreeBuilder(RenderTreeBuilder builder)
        {
            RenderTreeBuilder = builder;
        }

        public FluentRenderTreeBuilder(FluentRenderTreeBuilder fluentRenderTreeBuilder)
        {
            this.RenderTreeBuilder = fluentRenderTreeBuilder.RenderTreeBuilder;
            this.Parent = fluentRenderTreeBuilder;
        }

        public FluentComponentBuilder OpenComponent(Type componentType)
        {
            return new FluentComponentBuilder(this, componentType);
        }

        public FluentComponentBuilder OpenComponent<TComponentType>()
        {
            return new FluentComponentBuilder(this, typeof(TComponentType));
        }

        protected RenderTreeBuilder RenderTreeBuilder { get; }

        protected FluentRenderTreeBuilder Parent { get; }
    }

    public class FluentComponentBuilder : FluentRenderTreeBuilder
    {
        private readonly Type componentType;
        public int Index { get; private set; }

        public FluentComponentBuilder(FluentRenderTreeBuilder builder, Type componentType) : base(builder)
        {
            this.componentType = componentType;

            RenderTreeBuilder.OpenComponent(Index++, this.componentType);
        }

        //public FluentComponentBuilder AddAttribute(RenderTreeFrame frame)
        //{
        //    this.RenderTreeBuilder.AddAttribute(Index++, frame);
        //    return this;
        //}

        public FluentComponentBuilder AddAttribute<TArgument>(string name, EventCallback<TArgument> value)
        {
            this.RenderTreeBuilder.AddAttribute(Index++, name, value);
            return this;
        }

        public FluentComponentBuilder Invoke(Action<int, RenderTreeBuilder> action)
        {
            action(Index++, this.RenderTreeBuilder);
            return this;
        }
        // public FluentComponentBuilder AddAttribute(string name, EventCallback value)
        // {
        //     this.RenderTreeBuilder.AddAttribute(Index++, name, value);
        //     return this;
        // }
        // public FluentComponentBuilder AddAttribute(string name, Delegate value)
        // {
        //     this.RenderTreeBuilder.AddAttribute(Index++, name, value);
        //     return this;
        // }
        // public FluentComponentBuilder AddAttribute(string name, MulticastDelegate value)
        // {
        //     this.RenderTreeBuilder.AddAttribute(Index++, name, value);
        //     return this;
        // }
         public FluentComponentBuilder AddAttribute(string name, Func<Task> value)
         {
             this.RenderTreeBuilder.AddAttribute(Index++, name, value);
             return this;
         }
        public FluentComponentBuilder AddAttribute(string name, string? value)
        {
            this.RenderTreeBuilder.AddAttribute(Index++, name, value);
            return this;
        }


        public FluentComponentBuilder AddAttribute(string name)
        {
            this.RenderTreeBuilder.AddAttribute(Index++, name);
            return this;
        }

        public FluentComponentBuilder AddAttribute(string name, object value)
        {
            this.RenderTreeBuilder.AddAttribute(Index++, name, value);
            return this;
        }

        public FluentRenderTreeBuilder Close()
        {
            RenderTreeBuilder.CloseComponent();
            return this.Parent;
        }
    }
}