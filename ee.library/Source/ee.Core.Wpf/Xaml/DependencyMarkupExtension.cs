﻿using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ee.Core.Wpf.Xaml
{
    public abstract class DependencyMarkupExtension : MarkupExtension
    {
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            // 如果没有服务，则直接返回。
            var service = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (service == null)
            {
                return null;
            }

            // MarkupExtension 在样式模板中，返回 this 以延迟提供值。
            if (service.TargetObject.ToString().EndsWith("SharedDp"))
            {
                return this;
            }
            if (service.TargetObject is SetterBase)
            {
                throw new NotSupportedException(
                    "属性 Setter.Value 不支持 DependencyMarkupExtension，请尽量避免。");
            }

            var targetElement = service.TargetObject as FrameworkElement;
            var targetProperty = service.TargetProperty as DependencyProperty;

            if (targetElement == null || targetProperty == null)
            {
                throw new NotSupportedException(
                    @"DependencyMarkupExtension 仅支持设置在 FrameworkElement 的 DependencyProperty 上。");
            }

            var returnValue = ProvideValue(targetElement, targetProperty);
            if (returnValue is BindingBase binding)
            {
                targetElement.SetBinding(targetProperty, binding);
                return binding.ProvideValue(serviceProvider);
            }
            return returnValue;
        }

        protected abstract object ProvideValue(FrameworkElement target, DependencyProperty property);
    }
}
