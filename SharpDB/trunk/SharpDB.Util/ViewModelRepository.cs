using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDB.Util
{
    public class ViewModelRepository<TKey, TViewModel>
    {
        private readonly Func<TKey, TViewModel> _generator;
        private readonly Dictionary<TKey, TViewModel> _repository;

        public ViewModelRepository(Func<TKey, TViewModel> generator)
        {
            _generator = generator;
            _repository = new Dictionary<TKey, TViewModel>();
        }

        public TViewModel GetViewModel(TKey key)
        {
            TViewModel vm;
            if (!_repository.TryGetValue(key, out vm))
            {
                vm = _generator(key);
                _repository[key] = vm;
            }
            return vm;
        }

        public void PutViewModel(TKey key, TViewModel viewModel)
        {
            _repository[key] = viewModel;
        }
    }
}
