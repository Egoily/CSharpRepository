using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.Core.ServiceLocation
{
    public interface IDependencyLocator
    {

        //
        // Summary:
        //     Checks whether at least one instance of a given class is already created in the
        //     container.
        //
        // Type parameters:
        //   TClass:
        //     The class that is queried.
        //
        // Returns:
        //     True if at least on instance of the class is already created, false otherwise.
        bool ContainsCreated<TClass>();
        //
        // Summary:
        //     Checks whether the instance with the given key is already created for a given
        //     class in the container.
        //
        // Parameters:
        //   key:
        //     The key that is queried.
        //
        // Type parameters:
        //   TClass:
        //     The class that is queried.
        //
        // Returns:
        //     True if the instance with the given key is already registered for the given class,
        //     false otherwise.
        bool ContainsCreated<TClass>(string key);
        //
        // Summary:
        //     Gets a value indicating whether a given type T is already registered.
        //
        // Type parameters:
        //   T:
        //     The type that the method checks for.
        //
        // Returns:
        //     True if the type is registered, false otherwise.
        bool IsRegistered<T>();
        //
        // Summary:
        //     Gets a value indicating whether a given type T and a give key are already registered.
        //
        // Parameters:
        //   key:
        //     The key that the method checks for.
        //
        // Type parameters:
        //   T:
        //     The type that the method checks for.
        //
        // Returns:
        //     True if the type and key are registered, false otherwise.
        bool IsRegistered<T>(string key);


        void Register();

        //
        // Summary:
        //     Registers a given type for a given interface.
        //
        // Type parameters:
        //   TInterface:
        //     The interface for which instances will be resolved.
        //
        //   TClass:
        //     The type that must be used to create instances.
        void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class;
        //
        // Summary:
        //     Registers a given type for a given interface with the possibility for immediate
        //     creation of the instance.
        //
        // Parameters:
        //   createInstanceImmediately:
        //     If true, forces the creation of the default instance of the provided class.
        //
        // Type parameters:
        //   TInterface:
        //     The interface for which instances will be resolved.
        //
        //   TClass:
        //     The type that must be used to create instances.
        void Register<TInterface, TClass>(bool createInstanceImmediately)
            where TInterface : class
            where TClass : class;
        //
        // Summary:
        //     Registers a given type.
        //
        // Type parameters:
        //   TClass:
        //     The type that must be used to create instances.
        void Register<TClass>() where TClass : class;
        //
        // Summary:
        //     Registers a given type with the possibility for immediate creation of the instance.
        //
        // Parameters:
        //   createInstanceImmediately:
        //     If true, forces the creation of the default instance of the provided class.
        //
        // Type parameters:
        //   TClass:
        //     The type that must be used to create instances.
        void Register<TClass>(bool createInstanceImmediately) where TClass : class;
        //
        // Summary:
        //     Registers a given instance for a given type.
        //
        // Parameters:
        //   factory:
        //     The factory method able to create the instance that must be returned when the
        //     given type is resolved.
        //
        // Type parameters:
        //   TClass:
        //     The type that is being registered.
        void Register<TClass>(Func<TClass> factory) where TClass : class;
        //
        // Summary:
        //     Registers a given instance for a given type with the possibility for immediate
        //     creation of the instance.
        //
        // Parameters:
        //   factory:
        //     The factory method able to create the instance that must be returned when the
        //     given type is resolved.
        //
        //   createInstanceImmediately:
        //     If true, forces the creation of the default instance of the provided class.
        //
        // Type parameters:
        //   TClass:
        //     The type that is being registered.
        void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately) where TClass : class;
        //
        // Summary:
        //     Registers a given instance for a given type and a given key.
        //
        // Parameters:
        //   factory:
        //     The factory method able to create the instance that must be returned when the
        //     given type is resolved.
        //
        //   key:
        //     The key for which the given instance is registered.
        //
        // Type parameters:
        //   TClass:
        //     The type that is being registered.
        void Register<TClass>(Func<TClass> factory, string key) where TClass : class;
        //
        // Summary:
        //     Registers a given instance for a given type and a given key with the possibility
        //     for immediate creation of the instance.
        //
        // Parameters:
        //   factory:
        //     The factory method able to create the instance that must be returned when the
        //     given type is resolved.
        //
        //   key:
        //     The key for which the given instance is registered.
        //
        //   createInstanceImmediately:
        //     If true, forces the creation of the default instance of the provided class.
        //
        // Type parameters:
        //   TClass:
        //     The type that is being registered.
        void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately) where TClass : class;
        //
        // Summary:
        //     Resets the instance in its original states. This deletes all the registrations.
        void Reset();
        //
        // Summary:
        //     Unregisters a class from the cache and removes all the previously created instances.
        //
        // Type parameters:
        //   TClass:
        //     The class that must be removed.
        void Unregister<TClass>() where TClass : class;
        //
        // Summary:
        //     Removes the given instance from the cache. The class itself remains registered
        //     and can be used to create other instances.
        //
        // Parameters:
        //   instance:
        //     The instance that must be removed.
        //
        // Type parameters:
        //   TClass:
        //     The type of the instance to be removed.
        void Unregister<TClass>(TClass instance) where TClass : class;
        //
        // Summary:
        //     Removes the instance corresponding to the given key from the cache. The class
        //     itself remains registered and can be used to create other instances.
        //
        // Parameters:
        //   key:
        //     The key corresponding to the instance that must be removed.
        //
        // Type parameters:
        //   TClass:
        //     The type of the instance to be removed.
        void Unregister<TClass>(string key) where TClass : class;

    }
}
