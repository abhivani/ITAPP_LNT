using ITApp.Implement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ITApp.Web
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICommonRepository, CommonRepository>();
            container.RegisterType<IRoleMasterRepository, RoleMsterRepository>();
            container.RegisterType<IUSBRequestRepository, USBRequestRepository>();
            container.RegisterType<IDriveAccessRepository, DriveAccessRepository>();
            container.RegisterType<IDamageDeviceRepository, DamageDeviceRepository>();
            container.RegisterType<INewItRequisitionRepository, NewItRequisitionRepository>();

            container.RegisterType<IDeviceShiftingRepository, DeviceShiftingRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}