document.addEventListener("DOMContentLoaded", function () {
  const paymentForm = document.getElementById("paymentForm");
  const paymentMethod = document.getElementById("paymentMethod");
  const momoOption = document.getElementById("momoOption");
  const bankOptions = document.getElementById("bankOptions");
  const bankItems = document.querySelectorAll(".bank-item");
  const resultDiv = document.getElementById("result");

  let selectedBank = "";

  // Hiển thị các tùy chọn khi chọn phương thức thanh toán
  paymentMethod.addEventListener("change", function () {
    if (this.value === "momo") {
      momoOption.classList.remove("hidden");
      bankOptions.classList.add("hidden");
    } else if (this.value === "bank") {
      momoOption.classList.add("hidden");
      bankOptions.classList.remove("hidden");
    }
  });

  // Chọn ngân hàng và highlight mục đã chọn
  bankItems.forEach((item) => {
    item.addEventListener("click", function () {
      // Bỏ highlight cho tất cả các mục trước đó
      bankItems.forEach((i) => i.classList.remove("selected"));

      // Highlight mục hiện tại
      this.classList.add("selected");

      // Lưu ngân hàng đã chọn
      selectedBank = this.getAttribute("data-bank");
    });
  });

  // Xử lý sự kiện thanh toán
  paymentForm.addEventListener("submit", function (e) {
    e.preventDefault();

    const amount = document.getElementById("amount").value;
    const method = paymentMethod.value;

    if (method === "momo") {
      resultDiv.innerHTML = `Bạn đã chọn thanh toán qua <b>MoMo</b> với số tiền <b>${amount} VND</b>.`;
    } else if (method === "bank") {
      if (!selectedBank) {
        resultDiv.innerHTML = `<span style="color: red;">Vui lòng chọn ngân hàng!</span>`;
      } else {
        resultDiv.innerHTML = `Bạn đã chọn thanh toán qua <b>${selectedBank}</b> với số tiền <b>${amount} VND</b>.`;
      }
    }
  });
});
