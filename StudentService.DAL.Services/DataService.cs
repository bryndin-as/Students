using AutoMapper;
using StudentService.DAL.Contracts;
using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.Core;
using StudentService.DAL.DTO;
using StudentService.DAL.Model.Abstract;

namespace StudentService.DAL.Services
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentBaseDTO>> GetStudentsAsync()
        {
            var items = await _unitOfWork.StudentRepository.GetAllAsync();
            return _mapper.Map<List<StudentBaseDTO>>(items);
        }

        public async Task<IEnumerable<SubjectBaseDTO>> GetSubjectsAsync()
        {
            var items = await _unitOfWork.SubjectRepository.GetAllAsync();   
            return _mapper.Map<List<SubjectBaseDTO>>(items);

        }

        public async Task<IEnumerable<GradeBaseDTO>> GetGradesAsync()
        {
            var items = await _unitOfWork.GradeRepository.GetAllAsync();
            return _mapper.Map <List<GradeBaseDTO>>(items);
        }

        public async Task<int> CreateStudentAsync(StudentCreateDTO item)
        {
            return await CreateItemBaseAsync(item, _unitOfWork.StudentRepository);
        }

        public async Task<int> CreateSubjectAsync(SubjectCreateDTO item)
        {
            return await CreateItemBaseAsync(item, _unitOfWork.SubjectRepository);
        }

        public async Task<int> CreateGradeAsync(GradeCreateDTO item)
        {
            return await CreateItemBaseAsync(item, _unitOfWork.GradeRepository);
        }

        async Task<int> CreateItemBaseAsync<T, D>(D dim, IGenericRepository<T> repo)
    where D : class where T : ItemBase
        {
            var item = _mapper.Map<T>(dim);
            await repo.CreateAsync(item);
            return item.Id;
        }

        public async Task AddSeedTest(int count)
        {
            await _unitOfWork.TestFillDbRepository.SeedTestDataAsync(count);    
        }
    }
}