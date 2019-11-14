
using AutoMapper;
using System.Collections.Generic;
using System.Linq;


namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        private static IMapper mapper;
        public static IMapper Mapper => mapper ?? (mapper = AutoMapperConfiguration.Configuration.CreateMapper());


        public static DTO.Project Convert(Db.Entities.Project source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = Mapper.Map<DTO.Project>(source);

            destination.Account = Mapper.Map<DTO.ProjectAccount>(source?.Account ?? null);
            //---------------------
            var involvedClients = new List<DTO.Client>();
            if (source.InvolvedClients != null && source.InvolvedClients.Any())
            {
                foreach (var item in source.InvolvedClients)
                {
                    if (item.Client != null)
                    {
                        involvedClients.Add(Mapper.Map<DTO.Client>(item.Client));
                    }
                }
            }
            destination.InvolvedClients = involvedClients;
            //---------------------
            var todoList = new List<DTO.ProjectTodoItem>();
            if (source.TodoList != null && source.TodoList.Any())
            {
                foreach (var item in source.TodoList)
                {
                    todoList.Add(Mapper.Map<DTO.ProjectTodoItem>(item));
                }
            }
            destination.TodoList = todoList;
            //--------------------
            var progresses = new List<DTO.ProjectProgress>();
            if (source.Progresses != null && source.Progresses.Any())
            {
                foreach (var item in source.Progresses)
                {
                    progresses.Add(Mapper.Map<DTO.ProjectProgress>(item));
                }
            }
            destination.Progresses = progresses;

            return destination;
        }



        public static Db.Entities.ProjectProgress Convert(DTO.ProjectProgress source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectProgress>(source);
            target.InProject = project;
            return target;
        }
        public static Db.Entities.ProjectTodoItem Convert(DTO.ProjectTodoItem source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectTodoItem>(source);
            target.InProject = project;
            return target;
        }

        public static IList<DTO.Category> Convert(IList<DTO.PropertyPicker> source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new List<DTO.Category>();
            foreach (var item in source)
            {
                var category = Mapper.Map<DTO.Category>(item);
                target.Add(category);
            }
            return target;
        }

    }
}
