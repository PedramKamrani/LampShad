using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.SlideViewModel;
using Infractrucuer.EFCore.Repository;
using SMDomaim.Model.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.SlideApp
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideDomain _slideRepository;
        private readonly IFileUploader _fileUploder;
        public SlideApplication(ISlideDomain slideRepository, IFileUploader fileUploder)
        {
            _slideRepository = slideRepository;
            _fileUploder = fileUploder;
        }
        public OperationResult Create(CreateSlideViewModel command)
        {
            var opration=new OperationResult();
            string path = $"slides";
            var FileName =_fileUploder.Upload(command.Picture,path);
            Slide slide = new Slide(FileName, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text
                , command.BtnText, command.Link);
           _slideRepository.Create(slide);
            _slideRepository.SaveChanges();
            return opration.Succedded();
        }

        public OperationResult Edit(EditSlideViewModel command)
        {
            var slide = _slideRepository.Get(command.id);

            var oprtion = new OperationResult();
            var FileName = _fileUploder.Upload(command.Picture, "slides");
            if (slide == null)
                return oprtion.Failed(ApplicationMessages.RecordNotFound);
           
            slide.Edit(FileName, command.PictureAlt, command.PictureTitle,
                 command.Heading, command.Title, command.Text, command.Link, command.BtnText);
           
            _slideRepository.SaveChanges();
            return oprtion.Succedded();
        }

        public EditSlideViewModel GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var oprtion = new OperationResult();
            var slide = _slideRepository.Get(id);
            slide.Remove(slide.Id);
            _slideRepository.SaveChanges();
           return oprtion.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var oprtion = new OperationResult();
            var slide = _slideRepository.Get(id);
            slide.Restore(slide.Id);
            _slideRepository.SaveChanges();
            return oprtion.Succedded();
        }
    }
}
