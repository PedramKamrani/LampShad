using _0_FrameWork.BaseClass;
using Appliction.Construct.ViewModel.BannerVM;
using Infractrucuer.EFCore.Repository;
using SMDomaim.Model.BannerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.BannerApp
{
    public class BannerApplication : IBannerApplicationcs
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IFileUploader _fileUploader;
        public BannerApplication(IBannerRepository bannerRepository, IFileUploader fileUploader)
        {
            _bannerRepository = bannerRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Edit(EditBannerViewModel command)
        {
            var oprtion = new OperationResult();
            var banner = _bannerRepository.Get(command.Id);
            if (banner == null)
                return oprtion.Failed(ApplicationMessages.RecordNotFound);

            string path = $"banner";
            var Picture = _fileUploader.Upload(command.Picture, path);

             banner.Edit(Picture, command.PictureAlt, command.PictureTitle
                , command.Heading,command.Text,command.Link);
            _bannerRepository.SaveChanges();
            return oprtion.Succedded();
        }

        public List<BannerViewModel> GetAllBanner()
        {
            return _bannerRepository.GetAllBanner();
        }

        public EditBannerViewModel GetDetails(long id)
        {
            return _bannerRepository.GetDetails(id);
        }
    }
}
