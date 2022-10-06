<template>
  <div class="overlay">
    <div class="modal">
      <div class="modal__title">
        <span>Session Expired</span>
      </div>
      <div class="p-3">
        <p>You have left this browser idle for 10 minutes.</p>
        <p>{{ time / 1000 }} second left</p>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";

export default {
  data() {
    return {
      time: 30000,
      logoutStatus:true,
    };
  },
  methods: {
  
  },
  created() {

    let timerId = setInterval(() => {
      const currentUser= localStorage.getItem('user');
      if(typeof currentUser !=='undefined' && currentUser!== null){
        this.time -= 1000;
      if (!this.$store.state.idleVue.isIdle) clearInterval(timerId);

      if (this.time < 1) {
        clearInterval(timerId);
        // Your logout function should be over here
       let timerInterval
 
              Swal.fire({
                title: 'Session Expiration Alert!',
                //allowOutsideClick: false,
                html:
                  'Your Session will end in <strong></strong> seconds.<br/><br/>' +
                  
                  '<button id="stop" class="btn btn-primary">' +
                    'Continue with Session' +
                  '</button><br/><br/>' +
                  '<button id="resume" class="btn btn-danger" >' +
                    'LogOut' +
                  '</button><br/><br/>',
                timer: 30000,
                didOpen: () => {
                  const content = Swal.getHtmlContainer()
                  const $ = content.querySelector.bind(content)
               
                  const stop = $('#stop')
                  const resume = $('#resume')
                  //const toggle = $('#toggle')
                  //const increase = $('#increase')
               
                  Swal.showLoading()
               
                  stop.addEventListener('click', () => {
                    Swal.stopTimer()
                    Swal.close();
                    this.logoutStatus=false;
                  })
               
                  resume.addEventListener('click', () => {
                    Swal.resumeTimer()
                    Swal.close();
                    this.logoutStatus=true;
                    this.$router.push("/logout");
                    window.location = "/login";
                  })
                  timerInterval = setInterval(() => {
                    Swal.getHtmlContainer().querySelector('strong')
                      .textContent = (Swal.getTimerLeft() / 1000)
                        .toFixed(0)
                  }, 100)
                },
                willClose: () => {
                  clearInterval(timerInterval)
      

                }
              }).then(() => {
                  if(this.logoutStatus)
                  {
                    this.logoutStatus=true;
                    this.$router.push("/logout");
                    window.location = "/login";
                  }
                  else{
                    console.log('no redirection')
                     this.logoutStatus=true;
                  }
                
              });
      }
      }
      


    }, 1000);
  },
};
</script>

<style lang="postcss" scoped>
.overlay {
  position: fixed;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.3);
  z-index: 100;
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal {
  width: 500px;
  box-shadow: 1px 2px 4px rgba(153, 155, 168, 0.12);
  border-radius: 4px;
  @apply bg-white p-2;
}

.modal__title {
  color: #38404f;
  @apply flex items-center justify-between p-3 font-bold;
}
</style>