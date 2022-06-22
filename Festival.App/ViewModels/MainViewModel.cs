using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festival.App.ViewModels.Interfaces;

namespace Festival.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public IEventListViewModel EventListViewModel { get; }
        public IEventDetailViewModel EventDetailViewModel { get; }
        public IBandListViewModel BandListViewModel { get; }
        public IBandDetailViewModel BandDetailViewModel { get; }
        public IStageListViewModel StageListViewModel { get; }
        public IStageDetailViewModel StageDetailViewModel { get; }

        public MainViewModel
            (
            IEventListViewModel eventListViewModel, 
            IEventDetailViewModel eventDetailViewModel,
            IBandListViewModel bandListViewModel,
            IBandDetailViewModel bandDetailViewModel,
            IStageListViewModel stageListViewModel,
            IStageDetailViewModel stageDetailViewModel
            ) 
        {
            EventListViewModel = eventListViewModel;
            EventDetailViewModel = eventDetailViewModel;
            BandListViewModel = bandListViewModel;
            BandDetailViewModel = bandDetailViewModel;
            StageListViewModel = stageListViewModel;
            StageDetailViewModel = stageDetailViewModel;
        }
    }
}
