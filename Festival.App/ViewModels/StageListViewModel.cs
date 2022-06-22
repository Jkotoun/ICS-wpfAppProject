using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.BL.Models;

namespace Festival.App.ViewModels
{
    public class StageListViewModel : ViewModelBase, IStageListViewModel
    {

        private readonly StageFacade _stageFacade;
        private readonly IMediator _mediator;
        public StageListViewModel(
            StageFacade stageFacade,
            IMediator mediator)
        {
            _stageFacade = stageFacade;
            _mediator = mediator;

            StageSelectedCommand = new RelayCommand<StageDetailModel>(EventSelected);
            StageNewCommand = new RelayCommand(StageNew);

            mediator.Register<UpdateMessage<StageDetailModel>>(StageUpdate);
            mediator.Register<DeleteMessage<StageDetailModel>>(StageDeleted);
        }

        public ObservableCollection<StageDetailModel> Stages { get; set; } = new ObservableCollection<StageDetailModel>();

        public ICommand StageSelectedCommand { get; }
        public ICommand StageNewCommand { get; }

        private void StageNew() => _mediator.Send(new NewMessage<StageDetailModel>());

        private void EventSelected(StageDetailModel ev) => _mediator.Send(new SelectedMessage<StageDetailModel> { Id = ev.Id });

        private void StageUpdate(UpdateMessage<StageDetailModel> _) => Load();

        private void StageDeleted(DeleteMessage<StageDetailModel> _) => Load();

        public void Load()
        {
            this.Stages.Clear();
            var Stages = _stageFacade.GetAllDetail();
            foreach (var stage in Stages)
            {
                this.Stages.Add(stage);
            }
        }
    }
}
