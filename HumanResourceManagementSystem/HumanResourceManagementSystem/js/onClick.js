<script>
    ${function makeActive() {
        var element = document.getElementById("text");
        element.classList.add("active");
    },

        function myFunction(e) {
            if (document.querySelector('#navList li.a.active') !== null) {
                document.querySelector('#navList li.a.active').classList.remove('active');
            }
            e.target.className = ("active");
        }}
</script>