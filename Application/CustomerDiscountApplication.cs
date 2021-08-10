using _0_FrameWork.BaseClass;
using Application.Contract.ViewModel.CustomerDiscountVM;
using DisCountDoamin.CustomerDisountAgg;
using System;
using System.Collections.Generic;

namespace Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;
        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }
        public OperationResult Define(DefineCustomerDiscount command)
        {
            var option = new OperationResult();
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId
            &&x.DiscountRate==command.DiscountRate))
                return option.Failed(ApplicationMessages.DuplicatedRecord);
            var stardate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();

            var discunt = new CustomerDiscount(command.ProductId, command.DiscountRate,
                stardate, enddate,command.Reason);
            _customerDiscountRepository.Create(discunt);
            _customerDiscountRepository.SaveChanges();
            return option.Succedded();
        }

        public OperationResult Edit(EditCustoemrDiscount command)
        {
            var option = new OperationResult();
            var discunt = _customerDiscountRepository.Get(command.Id);
            if (discunt == null)
                return option.Failed(ApplicationMessages.RecordNotFound);

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId
            && x.DiscountRate == command.DiscountRate&&x.Id!=command.Id))
                return option.Failed(ApplicationMessages.DuplicatedRecord);

            var stardate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();
            discunt.Edit(command.ProductId, command.DiscountRate,
                stardate, enddate,command.Reason);
            _customerDiscountRepository.SaveChanges();
            return option.Succedded();
        }

        public EditCustoemrDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            return _customerDiscountRepository.Search(searchModel);
        }
    }
}
