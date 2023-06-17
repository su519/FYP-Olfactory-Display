close all

x1 = Lemon(:, 2); % Second column as x-values
y1 = Lemon(:, 4); % Fourth column as y-values

le = '#FFF86C';
la = '#c69aff';
o = '#fdbb5e';
i = '#c0dfff';
lg = '#d2f78c';
s = '#FFC3F5';

figure;

scatter(x1(1), y1(1), 300,'filled', "MarkerFaceColor", '#c69aff');
hold on
grid on
scatter(x1(2:7), y1(2:7), 300, 'filled', "MarkerFaceColor", '#fdbb5e');
scatter(x1(8:11), y1(8:11), 300, 'filled', "MarkerFaceColor", '#c0dfff');
scatter(x1(12:14), y1(12:14), 300, 'filled', "MarkerFaceColor", '#d2f78c');
scatter(x1(15), y1(15), 300, 'filled', "MarkerFaceColor",'#FFC3F5');
xlim([0,1]);
ylim([0,1]);
xlabel('Lemon Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Lemon','FontSize',14);
legend('Lavender','Orange','Iris','Lemongrass','Sakura','Location','Best');
saveas(gcf,'Lemon.png')

x1 = Lavender(:, 2); % Second column as x-values
y1 = Lavender(:, 4); % Fourth column as y-values

figure;

scatter(x1(1), y1(1), 300,'filled', "MarkerFaceColor", le);
hold on
grid on
scatter(x1(2:4), y1(2:4), 300, 'filled', "MarkerFaceColor", o);
scatter(x1(5:9), y1(5:9), 300, 'filled', "MarkerFaceColor", i);
scatter(x1(10:11), y1(10:11), 300, 'filled', "MarkerFaceColor", lg);
scatter(x1(12:16), y1(12:16), 300, 'filled', "MarkerFaceColor",s);
xlim([0,1]);
ylim([0,1]);
xlabel('Lavender Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Lavender','FontSize',14);
legend('Lemon','Orange','Iris','Lemongrass','Sakura','Location','Best');
saveas(gcf,'Lavender.png')

x2 = Orange(:, 2); % Second column as x-values
y2 = Orange(:, 4); % Fourth column as y-values

figure;

hold on
grid on
scatter(x2(1:6), y2(1:6), 300,'filled', "MarkerFaceColor", le);
scatter(x2(7:9), y2(7:9), 300,'filled', "MarkerFaceColor", la);
scatter(x2(10), y2(10), 300, 'filled', "MarkerFaceColor", i);
scatter(x2(11:14), y2(11:14), 300, 'filled', "MarkerFaceColor", lg);
scatter(x2(15:17), y2(15:17), 300, 'filled', "MarkerFaceColor",s);
xlim([0,1]);
ylim([0,1]);
xlabel('Orange Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Orange','FontSize',14);
legend('Lemon','Lavender','Iris','Lemongrass','Sakura','Location','Best');
saveas(gcf,'Orange.png')

x2 = Iris(:, 2); % Second column as x-values
y2 = Iris(:, 4); % Fourth column as y-values

figure;

hold on
grid on
scatter(x2(1:4), y2(1:4), 300,'filled', "MarkerFaceColor", le);
scatter(x2(5:9), y2(5:9), 300,'filled', "MarkerFaceColor", la);
scatter(x2(10), y2(10), 300, 'filled', "MarkerFaceColor", o);
scatter(x2(11:13), y2(11:13), 300, 'filled', "MarkerFaceColor", lg);
scatter(x2(14:15), y2(14:15), 300, 'filled', "MarkerFaceColor",s);
xlim([0,1]);
ylim([0,1]);
xlabel('Iris Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Iris','FontSize',14);
legend('Lemon','Lavender','Orange','Lemongrass','Sakura','Location','Best');
saveas(gcf,['Iris.png'])

x2 = Lemongrass(:, 2); % Second column as x-values
y2 = Lemongrass(:, 4); % Fourth column as y-values

figure;

hold on
grid on
scatter(x2(1:3), y2(1:3), 300,'filled', "MarkerFaceColor", le);
scatter(x2(4:5), y2(4:5), 300,'filled', "MarkerFaceColor", la);
scatter(x2(6:9), y2(6:9), 300, 'filled', "MarkerFaceColor", o);
scatter(x2(10:12), y2(10:12), 300, 'filled', "MarkerFaceColor", i);
scatter(x2(13:17), y2(13:17), 300, 'filled', "MarkerFaceColor",s);
xlim([0,1]);
ylim([0,1]);
xlabel('Lemongrass Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Lemongrass','FontSize',14);
legend('Lemon','Lavender','Orange','Iris','Sakura','Location','Best');
saveas(gcf,'Lemongrass.png')

x2 = Sakura(:, 2); % Second column as x-values
y2 = Sakura(:, 4); % Fourth column as y-values

figure;

hold on
grid on
scatter(x2(1), y2(1), 300,'filled', "MarkerFaceColor", le);
scatter(x2(2:6), y2(2:6), 300,'filled', "MarkerFaceColor", la);
scatter(x2(7:9), y2(7:9), 300, 'filled', "MarkerFaceColor", o);
scatter(x2(10:11), y2(10:11), 300, 'filled', "MarkerFaceColor", i);
scatter(x2(12:16), y2(12:16), 300, 'filled', "MarkerFaceColor",lg);
xlim([0,1]);
ylim([0,1]);
xlabel('Sakura Scent Intensity','FontSize',12, 'FontWeight','bold');
ylabel('Other Scent Intensity','FontSize',12, 'FontWeight','bold');
title('Sakura','FontSize',14);
legend('Lemon','Lavender','Orange','Iris','Lemongrass','Location','Best');
saveas(gcf,'Sakura.png')
