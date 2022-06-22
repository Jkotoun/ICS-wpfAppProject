using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Festival.App.Commands;
using Festival.App.Messages;
using Festival.App.Services;
using Festival.App.ViewModels.Interfaces;
using Festival.BL.Facades;
using Festival.BL.Models;

namespace Festival.App.ViewModels
{
    public class StageDetailViewModel : ViewModelBase, IStageDetailViewModel
    {
        private readonly StageFacade _stageFacade;
        private readonly IMediator _mediator;

        public StageDetailViewModel(
            StageFacade stageFacade,
            IMediator mediator)
        {
            _stageFacade = stageFacade;
            _mediator = mediator;

            SaveCommand = new RelayCommand(Save, CanSave);
            DeleteCommand = new RelayCommand(Delete, CanDelete);

            mediator.Register<SelectedMessage<StageDetailModel>>(StageSelected);
            mediator.Register<NewMessage<StageDetailModel>>(StageNew);
        }

        public StageDetailModel SelectedStage
        {
            get; set;
        }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private void StageSelected(SelectedMessage<StageDetailModel> ev) => Load(ev.Id);
        private void StageNew(NewMessage<StageDetailModel> _) => Load(Guid.Empty);

        public void Load(Guid id)
        {
            SelectedStage = _stageFacade.GetById(id) ?? new StageDetailModel();
            OnPropertyChanged(nameof(SelectedStage));
        }

        public void Save()
        {
            if (SelectedStage == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            _stageFacade.InsertOrUpdate(SelectedStage);
            _mediator.Send(new UpdateMessage<StageDetailModel> { Model = SelectedStage });
        }

        private bool CanSave()
        {
            return SelectedStage != null
            && !string.IsNullOrEmpty(SelectedStage.Name)
            && !string.IsNullOrEmpty(SelectedStage.Description)
            && !string.IsNullOrEmpty(SelectedStage.ImgUrl);
        }


        public void Delete()
        {

            if (SelectedStage.Id != Guid.Empty)
            {
                try
                {
                    _stageFacade.Delete(SelectedStage.Id);
                }
                catch
                {
                    MessageBox.Show("Deleting failed!");
                }

                _mediator.Send(new DeleteMessage<StageDetailModel>
                {
                    Model = SelectedStage
                });
                Load(Guid.Empty);
            }
        }

        private bool CanDelete()
        {
            return SelectedStage != null && SelectedStage.Id != Guid.Empty;
        }
    }
}
