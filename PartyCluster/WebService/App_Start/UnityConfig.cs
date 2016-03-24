// ------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace WebService
{
    using System.Fabric;
    using System.Web.Http;
    using Controllers;
    using Microsoft.Practices.Unity;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config, StatelessServiceContext serviceContext)
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<ClusterController>(
                new TransientLifetimeManager(),
                new InjectionConstructor(
#if LOCAL
                    new FakeCaptcha()));
#else
                    new Recaptcha(serviceContext)));
#endif

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}