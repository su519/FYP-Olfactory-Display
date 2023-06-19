close all

C1 = confusionmat(t1(:,1),t1(:,2));
label = {'Lemon','Lavender','Orange','Iris','Lemongrass','Sakura'};
figure
cm = confusionchart(C1,label,'RowSummary','row-normalized')
cm.Title = "Scent Identification Confusion Matrix"