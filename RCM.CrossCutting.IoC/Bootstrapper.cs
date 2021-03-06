﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RCM.Application.ApplicationInterfaces;
using RCM.Application.ApplicationServices;
using RCM.Application.EventServices;
using RCM.Application.Mappers;
using RCM.CrossCutting.MediatorServices;
using RCM.Domain.CommandHandlers.BancoCommandHandlers;
using RCM.Domain.CommandHandlers.ChequeCommandHandlers;
using RCM.Domain.CommandHandlers.CidadeCommandHandlers;
using RCM.Domain.CommandHandlers.ClienteCommandHandlers;
using RCM.Domain.CommandHandlers.DuplicataCommandHandlers;
using RCM.Domain.CommandHandlers.EmpresaCommandHandlers;
using RCM.Domain.CommandHandlers.FornecedorCommandHandlers;
using RCM.Domain.CommandHandlers.MarcaCommandHandlers;
using RCM.Domain.CommandHandlers.ProdutoCommandHandlers;
using RCM.Domain.CommandHandlers.VendaCommandHandlers;
using RCM.Domain.Commands.BancoCommands;
using RCM.Domain.Commands.ChequeCommands;
using RCM.Domain.Commands.CidadeCommands;
using RCM.Domain.Commands.ClienteCommands;
using RCM.Domain.Commands.DuplicataCommands;
using RCM.Domain.Commands.EmpresaCommands;
using RCM.Domain.Commands.FornecedorCommands;
using RCM.Domain.Commands.MarcaCommands;
using RCM.Domain.Commands.ProdutoCommands;
using RCM.Domain.Commands.VendaCommands;
using RCM.Domain.Core.Commands;
using RCM.Domain.Core.MediatorServices;
using RCM.Domain.DomainNotificationHandlers;
using RCM.Domain.EventHandlers;
using RCM.Domain.EventHandlers.BancoEventHandlers;
using RCM.Domain.EventHandlers.ChequeEventHandlers;
using RCM.Domain.EventHandlers.ClienteEventHandlers;
using RCM.Domain.EventHandlers.DuplicataEventHandlers;
using RCM.Domain.EventHandlers.EmpresaEventHandlers;
using RCM.Domain.EventHandlers.FornecedorEventHandlers;
using RCM.Domain.EventHandlers.MarcaEventHandlers;
using RCM.Domain.EventHandlers.ProdutoEventHandlers;
using RCM.Domain.EventHandlers.VendaEventHandlers;
using RCM.Domain.Events.BancoEvents;
using RCM.Domain.Events.ChequeEvents;
using RCM.Domain.Events.ClienteEvents;
using RCM.Domain.Events.DuplicataEvents;
using RCM.Domain.Events.EmpresaEvents;
using RCM.Domain.Events.FornecedorEvents;
using RCM.Domain.Events.MarcaEvents;
using RCM.Domain.Events.ProdutoEvents;
using RCM.Domain.Events.VendaEvents;
using RCM.Domain.Repositories;
using RCM.Domain.Repositories.EventRepositories;
using RCM.Domain.Services.Email;
using RCM.Domain.UnitOfWork;
using RCM.Infra.Data.Repositories;
using RCM.Infra.Data.Repositories.EventRepositories;
using RCM.Infra.Data.UnitOfWork;
using RCM.Infra.Services.Email;

namespace RCM.CrossCutting.IoC
{
    public sealed class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RegisterIntrastructureServices(services);
            RegisterMiscellaneous(services);
            RegisterRepositories(services);
            RegisterEventRepositories(services);
            RegisterApplicationServices(services);
            RegisterCommands(services);
            RegisterEventApplicationServices(services);
            RegisterEvents(services);
            RegisterPersistenceEvents(services);
            RegisterNotifications(services);
        }

        private static void RegisterIntrastructureServices(IServiceCollection services)
        {
            services.AddTransient<IEmailDispatcher, EmailDispatcher>();
            services.AddTransient<IEmailGenerator, EmailGenerator>();
        }

        private static void RegisterMiscellaneous(IServiceCollection services)
        {
            AutoMapperInitializer.Initialize();

            services.AddSingleton(typeof(IMapper), provider => Mapper.Instance);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAplicacaoRepository, AplicacaoRepository>();
            services.AddScoped<IBancoRepository, BancoRepository>();
            services.AddScoped<IChequeRepository, ChequeRepository>();
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IDuplicataRepository, DuplicataRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IEstadoRepository, EstadoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
        }

        private static void RegisterEventApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>();
        }

        private static void RegisterEventRepositories(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseApplicationService<,>), typeof(BaseApplicationService<,>));
            services.AddScoped<IAplicacaoApplicationService, AplicacaoApplicationService>();
            services.AddScoped<IBancoApplicationService, BancoApplicationService>();
            services.AddScoped<IChequeApplicationService, ChequeApplicationService>();
            services.AddScoped<ICidadeApplicationService, CidadeApplicationService>();
            services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            services.AddScoped<IDuplicataApplicationService, DuplicataApplicationService>();
            services.AddScoped<IEmpresaApplicationService, EmpresaApplicationService>();
            services.AddScoped<IEstadoApplicationService, EstadoApplicationService>();
            services.AddScoped<IFornecedorApplicationService, FornecedorApplicationService>();
            services.AddScoped<IMarcaApplicationService, MarcaApplicationService>();
            services.AddScoped<IProdutoApplicationService, ProdutoApplicationService>();
            services.AddScoped<IVendaApplicationService, VendaApplicationService>();
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddBancoCommand, CommandResult>, BancoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBancoCommand, CommandResult>, BancoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveBancoCommand, CommandResult>, BancoCommandHandler>();

            services.AddScoped<IRequestHandler<AddChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<BloquearChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<RepassarChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<CompensarChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<DevolverChequeCommand, CommandResult>, ChequeCommandHandler>();
            services.AddScoped<IRequestHandler<SustarChequeCommand, CommandResult>, ChequeCommandHandler>();

            services.AddScoped<IRequestHandler<AddCidadeCommand, CommandResult>, CidadeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCidadeCommand, CommandResult>, CidadeCommandHandler>();

            services.AddScoped<IRequestHandler<AddClienteCommand, CommandResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateClienteCommand, CommandResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveClienteCommand, CommandResult>, ClienteCommandHandler>();

            services.AddScoped<IRequestHandler<AddDuplicataCommand, CommandResult>, DuplicataCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDuplicataCommand, CommandResult>, DuplicataCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveDuplicataCommand, CommandResult>, DuplicataCommandHandler>();
            services.AddScoped<IRequestHandler<PagarDuplicataCommand, CommandResult>, DuplicataCommandHandler>();
            services.AddScoped<IRequestHandler<EstornarDuplicataCommand, CommandResult>, DuplicataCommandHandler>();

            services.AddScoped<IRequestHandler<AddOrUpdateEmpresaCommand, CommandResult>, EmpresaCommandHandler>();
            services.AddScoped<IRequestHandler<AttachEmpresaLogoCommand, CommandResult>, EmpresaCommandHandler>();

            services.AddScoped<IRequestHandler<AddFornecedorCommand, CommandResult>, FornecedorCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateFornecedorCommand, CommandResult>, FornecedorCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveFornecedorCommand, CommandResult>, FornecedorCommandHandler>();

            services.AddScoped<IRequestHandler<AddMarcaCommand, CommandResult>, MarcaCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateMarcaCommand, CommandResult>, MarcaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMarcaCommand, CommandResult>, MarcaCommandHandler>();
            
            services.AddScoped<IRequestHandler<AddProdutoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProdutoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProdutoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AttachProdutoAplicacaoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AddProdutoAplicacaoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProdutoAplicacaoCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AttachFornecedorCommand, CommandResult>, ProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveProdutoFornecedorCommand, CommandResult>, ProdutoCommandHandler>();

            services.AddScoped<IRequestHandler<AddVendaCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateVendaCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveVendaCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<AttachVendaProdutoCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveVendaProdutoCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<AttachVendaServicoCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveVendaServicoCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarVendaCommand, CommandResult>, VendaCommandHandler>();
            services.AddScoped<IRequestHandler<PagarParcelaVendaCommand, CommandResult>, VendaCommandHandler>();
        }

        private static void RegisterPersistenceEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddedBancoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedBancoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedBancoEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<BlockedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<PaidChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<PassedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RefundedChequeEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<CanceledChequeEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedClienteEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedClienteEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedClienteEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedDuplicataEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedDuplicataEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedDuplicataEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<PaidDuplicataEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RevertedPaymentDuplicataEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<UpdatedEmpresaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<AttachedEmpresaLogoEvent>, DomainEventPersistenceHandler>();
            
            services.AddScoped<INotificationHandler<AddedFornecedorEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedFornecedorEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedFornecedorEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedMarcaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedMarcaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedMarcaEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedProdutoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedProdutoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<AttachedProdutoAplicacaoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoAplicacaoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<AttachedProdutoFornecedorEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoFornecedorEvent>, DomainEventPersistenceHandler>();

            services.AddScoped<INotificationHandler<AddedVendaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<UpdatedVendaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<AttachedVendaProdutoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaProdutoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<AttachedVendaServicoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaServicoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<CheckedOutVendaEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<PaidInstallmentVendaEvent>, DomainEventPersistenceHandler>();
        }

        private static void RegisterEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<AddedBancoEvent>, BancoEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedBancoEvent>, BancoEventHandler>();
            services.AddScoped<INotificationHandler<RemovedBancoEvent>, BancoEventHandler>();

            services.AddScoped<INotificationHandler<AddedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<RemovedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<BlockedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<PaidChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<PassedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<RefundedChequeEvent>, ChequeEventHandler>();
            services.AddScoped<INotificationHandler<CanceledChequeEvent>, ChequeEventHandler>();

            services.AddScoped<INotificationHandler<AddedClienteEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedClienteEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<RemovedClienteEvent>, ClienteEventHandler>();

            services.AddScoped<INotificationHandler<AddedDuplicataEvent>, DuplicataEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedDuplicataEvent>, DuplicataEventHandler>();
            services.AddScoped<INotificationHandler<RemovedDuplicataEvent>, DuplicataEventHandler>();
            services.AddScoped<INotificationHandler<PaidDuplicataEvent>, DuplicataEventHandler>();
            services.AddScoped<INotificationHandler<RevertedPaymentDuplicataEvent>, DuplicataEventHandler>();

            services.AddScoped<INotificationHandler<UpdatedEmpresaEvent>, EmpresaEventHandler>();
            services.AddScoped<INotificationHandler<AttachedEmpresaLogoEvent>, EmpresaEventHandler>();

            services.AddScoped<INotificationHandler<AddedFornecedorEvent>, FornecedorEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedFornecedorEvent>, FornecedorEventHandler>();
            services.AddScoped<INotificationHandler<RemovedFornecedorEvent>, FornecedorEventHandler>();

            services.AddScoped<INotificationHandler<AddedMarcaEvent>, MarcaEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedMarcaEvent>, MarcaEventHandler>();
            services.AddScoped<INotificationHandler<RemovedMarcaEvent>, MarcaEventHandler>();

            services.AddScoped<INotificationHandler<AddedProdutoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedProdutoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<AttachedProdutoAplicacaoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoAplicacaoEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<AttachedProdutoFornecedorEvent>, ProdutoEventHandler>();
            services.AddScoped<INotificationHandler<RemovedProdutoFornecedorEvent>, ProdutoEventHandler>();

            services.AddScoped<INotificationHandler<AddedVendaEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedVendaEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<AttachedVendaProdutoEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaProdutoEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<AttachedVendaServicoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<RemovedVendaServicoEvent>, DomainEventPersistenceHandler>();
            services.AddScoped<INotificationHandler<CheckedOutVendaEvent>, VendaEventHandler>();
            services.AddScoped<INotificationHandler<PaidInstallmentVendaEvent>, VendaEventHandler>();
        }

        private static void RegisterNotifications(IServiceCollection services)
        {
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();
        }
    }
}
