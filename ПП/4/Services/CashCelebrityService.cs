using DAL_LES;
using DAL_LES.Interfaces;
using DAL_LES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashCelebrities
{
    public interface ICashCelebrities 
    {
        List<Celebrity> GetCelebrities();
        void PrintCashCelebs();
    }
    public class CashCelebrityService : ICashCelebrities
    {
        private readonly TimeSpan _duration;
        private List<Celebrity> _cachedCelebrities;
        private readonly ICelebrity _repo;
        private DateTime _lastRefresh;
        public CashCelebrityService(TimeSpan duration, ICelebrity repo)
        {
            _cachedCelebrities = new List<Celebrity>();
            _duration = duration;
            _lastRefresh = DateTime.MinValue;
            _repo = repo;
        }
        public List<Celebrity> GetCelebrities()
        {
            if (DateTime.Now - _lastRefresh >= _duration)
            {
                _cachedCelebrities = _repo.GetAllCelebrities();
                _lastRefresh = DateTime.Now;
            }
            return _cachedCelebrities ?? new List<Celebrity>();
        }
        public void PrintCashCelebs() {
            var celebrities_list = _cachedCelebrities.ToList();
            Console.WriteLine($"Cache time: {DateTime.Now:T}");
            Console.WriteLine($"Total celebrities: {celebrities_list.Count}");
            foreach (var c in celebrities_list)
            {
                Console.WriteLine($"Celebrity {c.Id}: {c.FullName}, {c.Nationality}");
            }
        }

    }
}
