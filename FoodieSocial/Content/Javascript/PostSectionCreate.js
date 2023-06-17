// Tạo một đối tượng div mới và gán các class và nội dung tương ứng
var newSection = document.createElement('section');
newSection.className = 'postSection';
newSection.innerHTML = `
    <div class="box">
        <div class="wrapper">
            <div class="header-wrap">
                <img src="~/Content/Images/avadefault.jpg" alt="" class="logo" style="width: 35px; height: 35px;">
                <div class="title">
                    <span>@Session["UserName"]</span>
                    <i class="fa-solid fa-x"></i>
                </div>
            </div>
            <div class="body-wrap">
                <img src="~/Content/Images/food.jpg" alt="">
            </div>
            <div class="body-title">
                <div class="wrapper-body">
                    <i class="fa-regular fa-heart"></i>
                    <i class="fa-regular fa-comment"></i>
                </div>
                <div class="wrapper-body">
                    <i class="fa-regular fa-bookmark"></i>
                </div>
            </div>
            <div class="status-text" style="margin-top: 25px;">
                <span>
                    Welcome to Foodie Social
                </span>
            </div>
        </div>
        <div class="sm-title">
            <div class="center-title">
                <div class="input-wrapper">
                    <input style="height: 45px; border-radius: 10px;" placeholder="Add comment..." />
                    <i class="fa-regular fa-paper-plane"></i>
                </div>
            </div>
        </div>
    </div>
    <hr />
`;

// Lấy ra vùng chứa id "posts-container"
var postsContainer = document.getElementById('posts-container');

// Chèn section mới vào vùng "posts-container"
postsContainer.appendChild(newSection);


$(document).ready(function () {
    // Tạo cấu trúc HTML cho section mới
    var newSection = `
        <section class="postSection">
            <!-- Các phần tử và nội dung của section -->
        </section>
        <hr />
    `;

    // Chèn section mới vào vùng "posts-container" bằng jQuery
    $('#posts-container').prepend(newSection);

    // Thêm dòng ngăn cách <hr/> sau section mới
    $('#posts-container').prepend('<hr/>');
});
