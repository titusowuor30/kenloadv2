<template>

   <div class="topSpace">
    <div class="home-btn d-none d-sm-block">
      <router-link to="/" class="text-dark"
        ><i class="mdi mdi-home-variant h2"></i
      ></router-link>
    </div>
    <div class="account-pages my-5 pt-sm-5">
      <div class="container">
        <div class="row">
          <div class="col-lg-12">
            <div class="text-center">
              
            </div>
          </div>
        </div>
        <div class="row align-items-center justify-content-center">
          <div class="col-md-8 col-lg-6 col-xl-5">
            <div class="card">
              <div class="card-body p-4">
                <div class="text-center mt-2">
                  <h5 class="text-primary">... !</h5>
                  <p class="text-muted">Sign in to continue to Kenload.</p>
                </div>
                <div class="p-2 mt-4">
                  <form>
                    <div class="mb-3">
                      <label for="username">Username</label>
                      <input
                        type="text"
                        class="form-control"
                        id="username"
                        value="Operator@weighing.co.ke"
                        placeholder="Enter username"
                      />
                    </div>

                    <div class="mb-3">
                      <div class="float-end">
                        <router-link to="/users/recoverpwd" class="text-muted">
                          Forgot password?</router-link
                        >
                      </div>
                      <label for="userpassword">Password</label>
                      <input
                        type="password"
                        class="form-control"
                        id="userpassword"
                        value="Operator@weighing.co.ke"
                        placeholder="Enter password"
                      />
                    </div>

                    <div class="form-check">
                      <input
                        type="checkbox"
                        class="form-check-input"
                        id="auth-remember-check"
                      />
                      <label class="form-check-label" for="auth-remember-check"
                        >Remember me</label
                      >
                    </div>

                    <div class="mt-3 text-end">
                      <router-link to="/users/addroles" class="text-muted">
                          
                      <button
                        class="btn btn-primary w-sm waves-effect waves-light"
                        type="submit"
                      >
                        Sign In
                      </button>
                      </router-link>
                    </div>

                    

                   
                  </form>
                </div>
              </div>
            </div>

            <div class="mt-5 text-center">
              <p>
                © {{ new Date().getFullYear() }} Kenload. Crafted <i class="mdi mdi-heart text-danger"></i> for KeNHA
              </p>
            </div>
          </div>
        </div>
        <!-- end row -->
      </div>
      <!-- end container -->
    </div>
  </div>
</template>

<script>
export default {

}
</script>


<style scoped>
.float-end{
  float: right;
  font-family: tahoma;
  font-weight: 1;
  color:grey;
}
body{
  background: #fff;
    margin-top: 60px;
  }

</style>