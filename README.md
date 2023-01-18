# DỰ ÁN TimeKeepAPI (chấm công)

## MÔ TẢ DỰ ÁN
> TimeKeep Web API giúp quản lý ngày/giờ làm việc của nhân viên trong công ty. Hỗ
trợ cho việc quản lý nhân viên.

## HƯỚNG DẪN TẢI VÀ CÀI ĐẶT

### TẢI DỰ ÁN

- Tải dự án từ link git bằng "git clone" hoặc file zip

> git clone https://gitlab.com/vanhoangtran241199/timekeepapi.git

### CẤU HÌNH KẾT NỐI DATABASE

- csdl : PostmanSQL
- cấu hình lại file appsettings.json 
- chỉnh lại 
  "ConnectionStrings": {
    "DefaultConnection": "UserID=postgres;Password=123;Host=localhost;Port=5432;Database=TimeKeeper_db;Pooling = true;"
  },

- Mở Package Manager consoler > nhập: update-database
- lưu ý nếu xóa file Migrations thì chạy dòng lệnh: add-migration intial 


### CÁC ƯU ĐIỂM CỦA DỰ ÁN

