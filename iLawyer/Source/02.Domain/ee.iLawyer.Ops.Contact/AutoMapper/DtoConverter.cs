
using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        private static IMapper mapper;
        public static IMapper Mapper => mapper ?? (mapper = AutoMapperConfiguration.Configuration.CreateMapper());


        public static DTO.ViewObjects.Project Convert(Db.Entities.Project source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = Mapper.Map<DTO.ViewObjects.Project>(source);

            destination.Account = Mapper.Map<DTO.ViewObjects.ProjectAccount>(source?.Account ?? null);
            //---------------------
            var involvedClients = new List<DTO.ViewObjects.Client>();
            if (source.InvolvedClients != null && source.InvolvedClients.Any())
            {
                foreach (var item in source.InvolvedClients)
                {
                    if (item.Client != null)
                    {
                        involvedClients.Add(Mapper.Map<DTO.ViewObjects.Client>(item.Client));
                    }
                }
            }
            destination.InvolvedClients = new ObservableCollection<DTO.ViewObjects.Client>(involvedClients);
            //---------------------
            var todoList = new List<DTO.ViewObjects.ProjectTodoItem>();
            if (source.TodoList != null && source.TodoList.Any())
            {
                foreach (var item in source.TodoList)
                {
                    todoList.Add(Mapper.Map<DTO.ViewObjects.ProjectTodoItem>(item));
                }
            }
            destination.TodoList = new ObservableCollection<DTO.ViewObjects.ProjectTodoItem>(todoList);
            //--------------------
            var progresses = new List<DTO.ViewObjects.ProjectProgress>();
            if (source.Progresses != null && source.Progresses.Any())
            {
                foreach (var item in source.Progresses)
                {
                    progresses.Add(Mapper.Map<DTO.ViewObjects.ProjectProgress>(item));
                }
            }
            destination.Progresses = new ObservableCollection<DTO.ViewObjects.ProjectProgress>(progresses);

            return destination;
        }
        public static Args.CreateProjectRequest ConvertToCreateProjectRequest(DTO.ViewObjects.Project source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = Mapper.Map<Args.CreateProjectRequest>(source);
            destination.InvolvedClientIds = source.InvolvedClients?.Select(x => x.Id)?.ToList();

            return destination;
        }
        public static Args.UpdateProjectRequest ConvertToUpdateProjectRequest(DTO.ViewObjects.Project source)
        {
            if (source == null)
            {
                return null;
            }

            var destination = Mapper.Map<Args.UpdateProjectRequest>(source);
            destination.InvolvedClientIds = source.InvolvedClients?.Select(x => x.Id)?.ToList();

            return destination;
        }

        public static Db.Entities.ProjectProgress Convert(DTO.ViewObjects.ProjectProgress source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectProgress>(source);
            target.InProject = project;
            return target;
        }
        public static Db.Entities.ProjectTodoItem Convert(DTO.ViewObjects.ProjectTodoItem source, Db.Entities.Project project)
        {
            if (source == null)
            {
                return null;
            }
            var target = Mapper.Map<Db.Entities.ProjectTodoItem>(source);
            target.InProject = project;
            return target;
        }


    }
}
