// Toggle Sidebar
const toggleBtn = document.getElementById("toggle-btn");
const sidebar = document.getElementById("sidebar");

// Khi click vào nút Toggle, ẩn/hiện sidebar
toggleBtn.addEventListener("click", () => {
  sidebar.classList.toggle("hide");
  document.querySelector(".content").style.marginLeft =
    sidebar.classList.contains("hide") ? "0" : "250px";
});

// JavaScript để xử lý việc mở rộng/thu gọn submenu
  document.querySelectorAll('.menu > li > a').forEach(menuItem => {
      menuItem.addEventListener('click', function (e) {
          const parentLi = this.parentElement;
          const submenu = parentLi.querySelector('.submenu');

          if (submenu) {
              e.preventDefault(); // Ngăn chặn chuyển trang nếu có submenu

              // Mở/đóng submenu
              submenu.style.display = submenu.style.display === 'block' ? 'none' : 'block';
            
              // Thêm/xoá class 'active' để xoay mũi tên
              parentLi.classList.toggle('active');
          }
      });
  });
