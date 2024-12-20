using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

		public async Task CreatePrduct(CreateProductDto createProductDto)
		{
			string query = "insert into Product (Title,Price,City,District,CoverImage,Address,Description,Type,DealOfTheDay,AdvertisementDate,ProductStatus,ProductCategory,EmployeeId) values (@Title,@Price,@City,@District,@CoverImage,@Address,@Description,@Type,@DealOfTheDay,@AdvertisementDate,@ProductStatus,@ProductCategory,@EmployeeId)";

			var parameters = new DynamicParameters();

			parameters.Add("@Title", createProductDto.Title);
			parameters.Add("@Price", createProductDto.Price);
			parameters.Add("@City", createProductDto.City);
			parameters.Add("@District", createProductDto.District);
			parameters.Add("@CoverImage", createProductDto.CoverImage);
			parameters.Add("@Address", createProductDto.Address);
			parameters.Add("@Description", createProductDto.Description);
			parameters.Add("@Type", createProductDto.Type);
			parameters.Add("@DealOfTheDay", createProductDto.DealOfTheDay);
			parameters.Add("@AdvertisementDate", createProductDto.AdvertisementDate);
			parameters.Add("@ProductStatus", createProductDto.ProductStatus);
			parameters.Add("@ProductCategory", createProductDto.ProductCategory);
			parameters.Add("@EmployeeId", createProductDto.EmployeeId);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task<List<ResultProductWithCategoryDto>> GellAllProductWithCategoryAsync()
        {
            string query = "Select ProductId,Title,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay,SlugUrl From Product inner join Category on Product.ProductCategory=Category.CategoryId";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From Product";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast3ProductWithCategoryDto>> GetLast3ProductAsync()
        {
            string query = "Select Top(3) ProductId,Title,Price,City,District,ProductCategory,CategoryName,AdvertisementDate,CoverImage,Description From Product Inner Join Category On Product.ProductCategory=Category.CategoryId Order By ProductId Desc";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast3ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = "Select Top(5) ProductId,Title,Price,City,District,ProductCategory,CategoryName,AdvertisementDate From Product Inner Join Category On Product.ProductCategory=Category.CategoryId Where Type='Kiralık' Order By ProductId Desc";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

		public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
		{
			string query = "Select ProductId,Title,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryId Where EmployeeId=@employeeId And ProductStatus=0";

			var parameters = new DynamicParameters();
			parameters.Add("@employeeId", id);

			using (var connection = _context.CreateConnection())
			{
				var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
				return values.ToList();
			}
		}

		public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = "Select ProductId,Title,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryId Where EmployeeId=@employeeId And ProductStatus=1";

            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueWithCategoryAsync()
        {
            string query = "Select ProductId,Title,Price,City,District,CategoryName,CoverImage,Type,Address,DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryId Where DealOfTheDay=1";

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            string query = "Select ProductId,Title,Price,City,District,Description,CategoryName,CoverImage,Type,Address,DealOfTheDay,AdvertisementDate,SlugUrl From Product inner join Category on Product.ProductCategory=Category.CategoryId Where ProductId=@productId";

            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductByProductIdDto>(query,parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id)
        {
            string query = "Select * From ProductDetails Where ProductId=@productId";

            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductDetailByIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
		{
			string query = "Update Product Set DealOfTheDay=0 Where ProductId=@productId";

			var parameters = new DynamicParameters();
			parameters.Add("@productId", id);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
		{
			string query = "Update Product Set DealOfTheDay=1 Where ProductId=@productId";

			var parameters = new DynamicParameters();
			parameters.Add("@productId", id);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

        public async Task<List<ResultProductWithSearchListDto>> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryId, string city)
        {
            string query = "Select * From Product Where Title like '%"+searchKeyValue+"%' And ProductCategory=@propertyCategoryId And City='@city'";

            var parameters = new DynamicParameters();

            //parameters.Add("@searchKeyValue", searchKeyValue);
            parameters.Add("@propertyCategoryId", propertyCategoryId);
            parameters.Add("@city", city);

            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithSearchListDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
