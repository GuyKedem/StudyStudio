using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Raven.Client;
using Raven.Client.Document;
using StudyStudio.Infrastructure.Commands;
using StudyStudio.Infrastructure.Queries;
using StudyStudio.Infrastructure.Scripts;

namespace StudyStudio.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly Func<IComponentContext, IDocumentStore> _buildDB;
        public InfrastructureModule(bool shouldFillWithDemoData)
        {
            _buildDB = c => DBUtils.SetupFreshStudyStudioDB();

            if (shouldFillWithDemoData)
            {
                var tmp = _buildDB;
                _buildDB = c => tmp(c).FillWithDemoData();
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_buildDB).SingleInstance();
            builder.RegisterType<ExerciseQueryService>().As<IExerciseQueryService>();
            builder.RegisterType<ExerciseCommandService>().As<IExerciseCommandService>();
            builder.RegisterType<AssignmentCommandService>().As<IAssignmentCommandService>();
        }
    }
}