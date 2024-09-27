<template>
  <div class="chart-container">
    <Doughnut :data="chartData" :options="chartOptions" />
  </div>
</template>

<script>
import { Doughnut } from 'vue-chartjs';
import { Chart as ChartJS, Title, Tooltip, Legend, ArcElement } from 'chart.js';

ChartJS.register(Title, Tooltip, Legend, ArcElement);

export default {
  name: 'DoughnutChart',
  components: {
    Doughnut
  },
  data() {
    return {
      chartData: {
        labels: ['Novos Suportes', 'Repetidos'],
        datasets: [
          {
            label: 'Suportes via Chatbot',
            backgroundColor: ['#007bff', '#e0e0e0'],
            data: [1420, 25]
          }
        ]
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            position: 'top',
          },
        },
      },
    };
  },
};
</script>

<style scoped>
.chart-container {
  position: relative;
  height: 200px;
}
</style>
