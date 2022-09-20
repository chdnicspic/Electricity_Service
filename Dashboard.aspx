<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
      	<div class="d-flex clearfix mt-4">
      		<h3 class="d-inline-block" style="font-size:32px;">Dashboard</h3>
      		<span class="ml-auto d-inline-block align-self-center"><button type="button" class="btn btn-primary b-btn"><span class="fas fa-download fa-sm"></span> Data</button></span>
      	</div>


        <!-- Content Row -->

        <div class="my-5" id="b-homedb">

			<div class="container">
				<h4 class="text-center mb-3 b-latest-data">Progress Report</h4>
				<div class="pl-4 text-right" style="font-size: 24px">
					<span class="mr-2" id="one-item-row" style="cursor: pointer;">
						<i class="fas fa-bars"></i>
					</span>
					<span class="mr-2" id="two-item-row" style="cursor: pointer;">
						<i class="fas fa-th-large"></i>
					</span>
					<span class="mr-2" id="three-item-row" style="cursor: pointer;">
						<i class="fas fa-th"></i>
					</span>
				</div>
				
				<div class="">
					<div class="row text-center pl-4" id="sortable-cards">
					    <div class="col-lg-6 col-sm-12 p-3 b-customize">
					        <div class="bg-light p-4 b-dbcard">
					        	<i class="fas fa-lightbulb-on position-absolute" style="font-size:35px; right: 40px; top: 40px;"></i> 
					        	<div class=""> 
					        		<p class="text-left font-weight-bold" style="font-size: 14px;">Energy Saved per year</p>
					        		<h4 class="text-left font-weight-bold" style="margin-top: -5px"><span class="counter-count">47087</span> mn kWh</h4>
					        		<div class="text-left" style="margin: 10px 0px 5px;">
					        			<span class="badge badge-success">+4%</span> 
					        			<span style="font-size:13px;"> From previous period</span>
					        		</div>
					        	</div>
					        </div>
					    </div>

					    <div class="col-lg-6 col-sm-12 p-3 b-customize">
					        <div class="bg-light p-4 b-dbcard">
					        	<i class="fas fa-rupee-sign position-absolute" style="font-size:35px; right: 40px; top: 40px;"></i> 
					        	<div class=""> 
					        		<p class="text-left font-weight-bold" style="font-size: 14px;">Cost Saving per year</p>
					        		<h4 class="text-left font-weight-bold" style="margin-top: -5px"><span class="counter-count">131339.59</span> Crores</h4>
					        		<div class="text-left" style="margin: 10px 0px 5px;">
					        			<span class="badge badge-success">2%</span> 
					        			<span style="font-size:13px;"> From previous period</span>
					        		</div>
					        	</div>
					        </div>
					    </div>

					    <div class="col-lg-6 col-sm-12 p-3 b-customize">
					        <div class="bg-light p-4 b-dbcard">
					        	<i class="fas fa-industry-alt position-absolute" style="font-size:35px; right: 40px; top: 40px;"></i> 
					        	<div class=""> 
					        		<p class="text-left font-weight-bold" style="font-size: 14px;">CO<sub>2</sub>	Reduction per year</p>
					        		<h4 class="text-left font-weight-bold" style="margin-top: -5px"><span class="counter-count">38140.634</span> Ktons CO<sub>2</sub></h4>
					        		<div class="text-left" style="margin: 10px 0px 5px;">
					        			<span class="badge badge-success">12%</span> 
					        			<span style="font-size:13px;"> From previous period</span>
					        		</div> 
					        	</div>
					        </div>
					    </div>

					    <div class="col-lg-6 col-sm-12 p-3 b-customize">
					        <div class="bg-light p-4 b-dbcard">
					        	<i class="fas fa-bolt position-absolute" style="font-size:35px; right: 40px; top: 40px;"></i> 
					        	<div class=""> 
					        		<p class="text-left font-weight-bold" style="font-size: 14px;">Avoided Peak Demand</p>
					        		<h4 class="text-left font-weight-bold" style="margin-top: -5px"><span class="counter-count">9428</span> MW</h4>
					        		<div class="text-left" style="margin: 10px 0px 5px;">
					        			<span class="badge badge-success">+10%</span> 
					        			<span style="font-size:13px;"> From previous period</span>
					        		</div>
					        	</div>
					        </div>
					    </div>

					</div>
				</div>

			</div>

		</div>




        <div class="row my-5 mx-sm-5">
        	<div class="col-md-6">
        		<h4 class="text-center">Agency Distribution Counts</h4>
        		<canvas id="verticalBarChart2" width="400" height="400"></canvas>
        	</div>
        	<div class="col-md-6">
        		<h4 class="text-center">Agency Distribution Counts</h4>
        		<canvas id="doughnutChart2" width="400" height="400"></canvas>
        	</div>
        </div>
        

    </div>
</asp:Content>

