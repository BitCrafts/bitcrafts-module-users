using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;
using BitCrafts.Module.Users.Abstraction.Presenters.Family;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;
using BitCrafts.Module.Users.Abstraction.Views.Family;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters.Family;

public class FamilyPresenter : BasePresenter<IFamilyView>, IFamilyPresenter
{
    private readonly IEventAggregator _eventAggregator;
    private readonly ICreateFamilyUseCase _createFamilyUseCase;
    private readonly IGetFamilyUseCase _getFamilyUseCase;
    private readonly IUpdateFamilyUseCase _updateFamilyUseCase;
    private readonly IDeleteFamilyUseCase _deleteFamilyUseCase;
    private readonly IGetFamiliesUseCase _getFamiliesUseCase;

    public FamilyPresenter(IFamilyView view, ILogger<BasePresenter<IFamilyView>> logger,
        IEventAggregator eventAggregator, ICreateFamilyUseCase createFamilyUseCase,
        IGetFamilyUseCase getFamilyUseCase, IUpdateFamilyUseCase updateFamilyUseCase,
        IDeleteFamilyUseCase deleteFamilyUseCase, IGetFamiliesUseCase getFamiliesUseCase)
        : base(view, logger)
    {
        _eventAggregator = eventAggregator;
        _createFamilyUseCase = createFamilyUseCase;
        _getFamilyUseCase = getFamilyUseCase;
        _updateFamilyUseCase = updateFamilyUseCase;
        _deleteFamilyUseCase = deleteFamilyUseCase;
        _getFamiliesUseCase = getFamiliesUseCase;
    }

    protected override void OnInitialize()
    {
        _eventAggregator.Subscribe<CreateFamilyEvent>(OnCreateFamilyRequested);
        _eventAggregator.Subscribe<UpdateFamilyEvent>(OnUpdateFamilyRequested);
        _eventAggregator.Subscribe<DeleteFamilyEvent>(OnDeleteFamilyRequested);
        _eventAggregator.Subscribe<GetFamiliesEvent>(OnGetFamiliesForUserRequested);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _eventAggregator.Unsubscribe<CreateFamilyEvent>(OnCreateFamilyRequested);
            _eventAggregator.Unsubscribe<UpdateFamilyEvent>(OnUpdateFamilyRequested);
            _eventAggregator.Unsubscribe<DeleteFamilyEvent>(OnDeleteFamilyRequested);
            _eventAggregator.Unsubscribe<GetFamiliesEvent>(OnGetFamiliesForUserRequested);
        }

        base.Dispose(disposing);
    }

    private void OnCreateFamilyRequested(CreateFamilyEvent request)
    {
        throw new NotImplementedException();
    }

    private void OnUpdateFamilyRequested(UpdateFamilyEvent obj)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteFamilyRequested(DeleteFamilyEvent obj)
    {
        throw new NotImplementedException();
    }

    private void OnGetFamiliesForUserRequested(GetFamiliesEvent obj)
    {
        throw new NotImplementedException();
    }
}