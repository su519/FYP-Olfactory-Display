close all
figure

C1 = confusionmat(test1_equal(:,1),test1_equal(:,2));

cm = confusionchart(C1,label,'RowSummary','row-normalized')
cm.Title = "Scent Identification Confusion Matrix"