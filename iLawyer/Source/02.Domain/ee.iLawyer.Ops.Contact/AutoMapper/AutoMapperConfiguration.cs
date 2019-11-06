using AutoMapper;
using System.Collections.Generic;
using System.Reflection;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration configuration;
        public static MapperConfiguration Configuration => configuration ?? (configuration = Initialize());




        public static MapperConfiguration Initialize()
        {
            configuration = new MapperConfiguration(cfg => 
            {
                cfg.AddMaps(GetAssembly());
                //cfg.AddProfiles(GetProfiles());
            });
            //configuration.AssertConfigurationIsValid();

            return configuration;
        }


        private static Assembly GetAssembly()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly;
        }
        private static IEnumerable<Profile> GetProfiles()
        {
            IEnumerable<Profile> profiles = new Profile[] { new MappingProfile() };
            return profiles;
        }


    }
}
