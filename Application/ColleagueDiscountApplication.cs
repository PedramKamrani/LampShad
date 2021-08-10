using _0_FrameWork.BaseClass;
using Application.Contract.ViewModel.ColleagueDiscountVM;
using DisCountDoamin.ColleagueDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ColleagueDiscountApplication : IColleagueApplication
    {

        private readonly IColleagueRepository _repository;
        public ColleagueDiscountApplication(IColleagueRepository repository)
        {
            _repository = repository;
        }
        public OperationResult Define(DefineColleagueDiscount command)
        {
            var opration = new OperationResult();

            if (_repository.Exists(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return opration.Failed(ApplicationMessages.DuplicatedRecord);

            var discuont = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _repository.Create(discuont);
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public OperationResult Edit(EditcolleagueDiscountViewModel command)
        {
            var opration = new OperationResult();
            var discuontcolleague = _repository.Get(command.Id);
            if (discuontcolleague == null)
                return opration.Failed(ApplicationMessages.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == command.ProductId && 
            x.DiscountRate == command.DiscountRate&&x.Id!=command.Id))
                return opration.Failed(ApplicationMessages.DuplicatedRecord);

            discuontcolleague.Edit(command.ProductId, command.DiscountRate);
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public EditcolleagueDiscountViewModel GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var opration = new OperationResult();
            var discuontcolleague = _repository.Get(id);
            if (discuontcolleague == null)
                return opration.Failed(ApplicationMessages.RecordNotFound);

            discuontcolleague.Remove(discuontcolleague.Id);
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var opration = new OperationResult();
            var discuontcolleague = _repository.Get(id);
            if (discuontcolleague == null)
                return opration.Failed(ApplicationMessages.RecordNotFound);

            discuontcolleague.Restore(discuontcolleague.Id);
            _repository.SaveChanges();
            return opration.Succedded();
        }

        public List<ColleagueDiscounViewModel> Search(ColleagueSearchModel searchModel)
        {
            return _repository.search(searchModel);
        }
    }
}
