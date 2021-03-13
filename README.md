# ecloudvalley
  伊雲谷面試

# 架構設計
  請參考 https://github.com/a80823111/ecloudvalley/blob/main/Document/StructuralDesign.png

# 使用方法
	1.使用RestoredCsvSchedule Console Application匯入RowData
	  修改RowData檔案路徑,設定檔路徑 RestoredCsvSchedule/Configuration/RestoredCsvScheduleSettings.json
	  * Notice. 匯入資料有做Transaction , 因此檔案內只要有一筆資料錯誤就不會匯入

	2.開始使用Api查詢報告
	3.以使用docker自行架設Webapi , 位置為 http://122.116.211.180:85/ , 使用時注意網路環境是否可以連線

# Api說明
   說明 : 所有Api Response皆以進行封裝統一格式 <br>
   Swagger Url : http://122.116.211.180:85/swagger
   
    1.Api名稱:Get lineItem/UnblendedCost grouping by product/productname
	   Urls : /api/Report/UnblendedCostReport/{usageAccountId}
	   Method:Get
	   參數: 
			名稱 - usageAccountId
			說明 - {lineitem/usageaccountid} 
			type - long
			nullable: false
			  
			名稱 - currentPage
			說明 - 當前頁數 (指定要看第幾頁)
			type - int
			nullable: true
			
			名稱 - pageCount
			說明 - 每頁幾筆
			type - int
			nullable: true
		
		回傳資訊:
			名稱 - result
			說明 - UnblendedCost報告結果
			type - ArrayObject
			Object說明 - productName, product/ProductName
						 totalUnblendedCost, sum(lineitem/unblendedcost)
			
			名稱 - pageInfo
			說明 - 分頁資訊
			type - Object
			Object說明 - totalPage, 總頁數
						 currentPage, 當前頁數 (指定要看第幾頁)
						 pageCount, 每頁幾筆
		
		範例:
			Url : http://122.116.211.180:85/api/Report/UnblendedCostReport/147878817734?currentPage=1&pageCount=3
			回傳:{
					"result": [
						{
							"productName": "000 requests\"",
							"totalUnblendedCost": 0.00227
						},
						{
							"productName": " t2.large reserved instance applied\"",
							"totalUnblendedCost": 0
						},
						{
							"productName": "000",
							"totalUnblendedCost": 0
						}
					],
					"pageInfo": {
						"totalPage": 3,
						"currentPage": 1,
						"pageCount": 3
					}
				}
			
	------------------------------------------------------------------------------------------------------------------------------------
	
	2.Api名稱:Get daily lineItem/UsageAmount grouping by product/productname
		Urls : /api/Report/UsageAmountDailyReport/{usageAccountId}
		Method:Get
		參數: 
			名稱 - usageAccountId
			說明 - {lineitem/usageaccountid} 
			type - long
			nullable: false
			  
			名稱 - currentPage
			說明 - 當前頁數 (指定要看第幾頁)
			type - int
			nullable: true
			
			名稱 - pageCount
			說明 - 每頁幾筆
			type - int
			nullable: true
		
		回傳資訊:
			名稱 - result
			說明 - UsageAmountDaily報告結果
			type - ArrayObject
			Object說明 - productName, product/ProductName
						 dailyDetails - 每日報告細節 (ArrayObject)
								usageStartDate - lineItem/UsageStartDate
								usageEndDate - lineItem/lineItem/UsageEndDate
								totalUsageAmount - sum(lineItem/UsageAmount)
			
			名稱 - pageInfo
			說明 - 分頁資訊
			type - Object
			Object說明 - totalPage, 總頁數
						 currentPage, 當前頁數 (指定要看第幾頁)
						 pageCount, 每頁幾筆
		
		範例:
			Url : http://122.116.211.180:85/api/Report/UsageAmountDailyReport/147878817734?currentPage=1&pageCount=3
			回傳: {
					"result": [
						{
							"productName": "000 requests\"",
							"dailyDetails": [
								{
									"usageStartDate": "2020-04-14T00:00:00",
									"usageEndDate": "2020-04-14T00:00:00",
									"totalUsageAmount": 1
								},
								{
									"usageStartDate": "2020-04-24T00:00:00",
									"usageEndDate": "2020-04-24T00:00:00",
									"totalUsageAmount": 76
								},
								{
									"usageStartDate": "2020-04-06T00:00:00",
									"usageEndDate": "2020-04-06T00:00:00",
									"totalUsageAmount": 75
								},
								{
									"usageStartDate": "2020-04-18T00:00:00",
									"usageEndDate": "2020-04-18T00:00:00",
									"totalUsageAmount": 75
								}
							]
						},
						...
					],
					"pageInfo": {
						"totalPage": 3,
						"currentPage": 1,
						"pageCount": 3
					}
				}
		
# DB schema
    資料表說明
	* Notice. 因對此RowData意義不清楚 , 因此設計上不使用組合鍵去綁住商業邏輯 , 另外也有分析提供之RowData , 有一個欄位identity/LineItemId有重複因此不當作主鍵
	
    欄位名稱       類型          主鍵   nullable
	Id             bigint        true   false,
	PayerAccountId bigint        false  false,
	UnblendedCost  float         false  true,
	UnblendedRate  float         false  true,
	UsageAccountId bigint        false  false,
	UsageAmount    float         false  false,
	UsageStartDate datetime      false  false,
	UsageEndDate   datetime      false  false,
	ProductName    nvarchar(500) false  false
	
	Sql Commend
	CREATE TABLE lineitem (
	  Id bigint primary key identity,
	  PayerAccountId bigint not null,
	  UnblendedCost float,
	  UnblendedRate float,
	  UsageAccountId bigint not null,
	  UsageAmount float not null,
	  UsageStartDate datetime not null,
	  UsageEndDate datetime not null,
	  ProductName nvarchar(500)
	);
	
# How to reduce the response time / improve performance
	1. 使用快取將報告快取
	   以自己家用電腦來說 , 第一次查詢約2s , 第二次查詢都在20ms左右
	   
