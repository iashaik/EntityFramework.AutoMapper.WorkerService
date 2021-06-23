using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EntityFramework.Example;
using Automapper.EntityFramework.Example.Model;
using AutoMapper.EntityFrameworkCore;

namespace EntityFramework.AutoMapper.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public Worker(ILogger<Worker> logger, IMapper mapper, ApplicationContext context)
        {
            _logger = logger;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await AddWithoutPersist();
            Read();
            await AddWithPersist();
        }

        

        private async Task AddWithoutPersist()
        {

            Console.WriteLine("Start Insert without persist");

            NameEntity entity = _mapper.Map<NameEntity>(new NameModel() { Name = "Adding without Persist" });
            _context.Set<NameEntity>().Add(entity);
            await _context.SaveChangesAsync();

            Console.WriteLine("Done Insert without persist");
        }


        private void Read()
        {
            int result = _context.Set<NameEntity>()
                                 .Count();

            Console.WriteLine($"Found {result} items");

        }

        private async Task AddWithPersist()
        {
            try
            {

                Console.WriteLine("Start Insert With Persist");

                await _context
                          .Set<NameEntity>()
                          .Persist(_mapper)
                          .InsertOrUpdateAsync(new NameModel() { Name = "Adding with Persist" });

                Console.WriteLine("Done Insert with persist");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
